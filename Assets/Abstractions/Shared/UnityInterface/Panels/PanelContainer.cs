using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Abstractions.Shared.UnityInterface
{
	public class PanelContainer : WindowContainer
	{
		private readonly List<IPanelContainerCallbackReceiver> _callbackReceivers = new();
		private readonly Dictionary<IViewPresenter, ViewRef<PanelView>> _panelViews = new();

		protected override void OnDestroy()
		{
			base.OnDestroy();

			foreach ((var panelView, var assetPath) in _panelViews.Values)
			{
				DestroyAndForget(panelView, assetPath, PoolingPolicy.DisablePooling).Forget();
			}

			_panelViews.Clear();
		}

		public void AddCallbackReceiver(IPanelContainerCallbackReceiver callbackReceiver)
		{
			_callbackReceivers.Add(callbackReceiver);
		}

		public void RemoveCallbackReceiver(IPanelContainerCallbackReceiver callbackReceiver)
		{
			_callbackReceivers.Remove(callbackReceiver);
		}

		public void Show<TPanelView>(IViewPresenter presenter, PanelViewConfig config) where TPanelView : PanelView
		{
			ShowAndForget<TPanelView>(presenter, config).Forget();
		}

		private async UniTask ShowAndForget<TPanelView>(IViewPresenter presenter, PanelViewConfig config) where TPanelView : PanelView
		{
			await ShowAsyncInternal<TPanelView>(presenter, config);
		}

		public async UniTask ShowAsync<TPanelView>(IViewPresenter presenter, PanelViewConfig config) where TPanelView : PanelView
		{
			await ShowAsyncInternal<TPanelView>(presenter, config);
		}

		public void Hide(IViewPresenter presenter, bool playAnimation = true)
		{
			HideAndForget(presenter, playAnimation).Forget();
		}

		private async UniTaskVoid HideAndForget(IViewPresenter presenter, bool playAnimation)
		{
			await HideAsyncInternal(presenter, playAnimation);
		}

		public async UniTask HideAsync(IViewPresenter presenter, bool playAnimation = true)
		{
			await HideAsyncInternal(presenter, playAnimation);
		}

		public void HideAll(bool playAnimation = true)
		{
			HideAllAndForget(playAnimation).Forget();
		}

		private async UniTaskVoid HideAllAndForget(bool playAnimation = true)
		{
			await HideAllAsyncInternal(playAnimation);
		}

		public async UniTask HideAllAsync(bool playAnimation = true)
		{
			await HideAllAsyncInternal(playAnimation);
		}

		private async UniTask ShowAsyncInternal<TPanelView>(IViewPresenter presenter, PanelViewConfig config) where TPanelView : PanelView
		{
			var assetPath = config.Config.AssetPath;
			if (assetPath == null)
			{
				throw new ArgumentNullException(nameof(assetPath));
			}

			if (_panelViews.TryGetValue(presenter, out var viewRef))
			{
				Debug.LogWarning($"Cannot transition because the {typeof(TPanelView).Name} at `{assetPath}` is already showing.", viewRef.View);
				return;
			}

			if (Settings.EnableInteractionInTransition == false)
			{
				Interactable = false;
			}

			var panelView = await GetViewAsync<TPanelView>(config.Config);
			Add(presenter, panelView, config.Config.PoolingPolicy);
			config.Config.ViewLoadedCallback?.Invoke(panelView);

			await panelView.AfterLoadAsync(RectTransform);

			panelView.SetSortingLayer(config.SortingLayer, config.OrderInLayer);

			foreach (var callbackReceiver in _callbackReceivers)
			{
				callbackReceiver.BeforeShow(panelView);
			}

			await panelView.BeforeEnterAsync(true);

			await panelView.EnterAsync(true, config.Config.PlayAnimation);

			panelView.AfterEnter(true);

			foreach (var callbackReceiver in _callbackReceivers)
			{
				callbackReceiver.AfterShow(panelView);
			}

			if (Settings.EnableInteractionInTransition == false)
			{
				Interactable = true;
			}
		}

		private async UniTask HideAsyncInternal(IViewPresenter presenter, bool playAnimation)
		{
			if (_panelViews.TryGetValue(presenter, out var viewRef) == false)
			{
				Debug.LogError($"Cannot transition because there is no panel loaded " + $"on the stack by the asset path `{presenter}`");
				return;
			}

			if (Settings.EnableInteractionInTransition == false)
			{
				Interactable = false;
			}

			var panelView = viewRef.View;
			panelView.Settings = Settings;

			foreach (var callbackReceiver in _callbackReceivers)
			{
				callbackReceiver.BeforeHide(panelView);
			}

			await panelView.BeforeEnterAsync(false);

			await panelView.EnterAsync(false, playAnimation);

			Remove(presenter);

			panelView.AfterEnter(false);

			foreach (var callbackReceiver in _callbackReceivers)
			{
				callbackReceiver.AfterHide(panelView);
			}

			await panelView.BeforeReleaseAsync();

			DestroyAndForget(panelView, presenter.ViewConfig.AssetPath, viewRef.PoolingPolicy).Forget();

			if (Settings.EnableInteractionInTransition == false)
			{
				Interactable = true;
			}
		}

		private async UniTask HideAllAsyncInternal(bool playAnimation = true)
		{
			var keys = new List<IViewPresenter>(_panelViews.Count);
			var tasks = new List<UniTask>(_panelViews.Count);

			keys.AddRange(_panelViews.Keys);

			var count = keys.Count;
			for (var i = 0; i < count; i++)
			{
				var task = HideAsyncInternal(keys[i], playAnimation);
				tasks.Add(task);
			}

			await UniTask.WhenAll(tasks);
		}

		private void Add(IViewPresenter presenter, PanelView panel, PoolingPolicy poolingPolicy)
		{
			if (presenter == null)
			{
				throw new ArgumentNullException(nameof(presenter));
			}

			if (_panelViews.TryGetValue(presenter, out var viewRef))
			{
				if (panel != viewRef.View)
				{
					Debug.LogWarning($"Another {nameof(PanelView)} is existing for `{presenter}`", viewRef.View);
				}

				return;
			}

			viewRef = new ViewRef<PanelView>(panel, presenter.ViewConfig.AssetPath, poolingPolicy);
			_panelViews.Add(presenter, viewRef);

			if (panel.TryGetTransform(out var trans))
			{
				transform.AddChild(trans);
			}
		}

		private bool Remove(IViewPresenter presenter)
		{
			if (presenter == null)
			{
				throw new ArgumentNullException(nameof(presenter));
			}

			if (_panelViews.TryGetValue(presenter, out var panelView))
			{
				if (panelView.TryGetTransform(out var trans))
				{
					transform.RemoveChild(trans);
				}

				return _panelViews.Remove(presenter);
			}

			return false;
		}
	}
}
