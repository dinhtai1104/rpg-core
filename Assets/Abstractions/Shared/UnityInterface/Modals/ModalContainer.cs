using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Abstractions.Shared.UnityInterface
{
	public class ModalContainer : WindowContainer
	{
		public bool IsInTransition { get; private set; }

		public IReadOnlyList<ViewRef<ModalView>> ModalViews => _modalViews;

		public IReadOnlyList<ViewRef<ModalBackdrop>> Backdrops => _backdrops;

		public ViewRef<ModalView> CurrentModalView => _modalViews[^1];

		private readonly List<IModalContainerCallbackReceiver> _callbackReceivers = new();
		private readonly List<ViewRef<ModalView>> _modalViews = new();
		private readonly List<ViewRef<ModalBackdrop>> _backdrops = new();
		private bool _disableBackdrop;

		protected override void OnInitialize()
		{
			_callbackReceivers.AddRange(GetComponents<IModalContainerCallbackReceiver>());
			_disableBackdrop = Settings.DisableModalBackdrop;
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();

			var modalCount = _modalViews.Count;
			for (var i = 0; i < modalCount; i++)
			{
				(var modal, var resourcePath) = _modalViews[i];
				DestroyAndForget(modal, resourcePath, PoolingPolicy.DisablePooling).Forget();
			}

			_modalViews.Clear();

			var backdropCount = _backdrops.Count;
			for (var i = 0; i < backdropCount; i++)
			{
				(var backdrop, var resourcePath) = _backdrops[i];
				DestroyAndForget(backdrop, resourcePath, PoolingPolicy.DisablePooling).Forget();
			}

			_backdrops.Clear();
		}

		public void AddCallbackReceiver(IModalContainerCallbackReceiver callbackReceiver)
		{
			_callbackReceivers.Add(callbackReceiver);
		}

		public void RemoveCallbackReceiver(IModalContainerCallbackReceiver callbackReceiver)
		{
			_callbackReceivers.Remove(callbackReceiver);
		}

		public bool FindIndexOfRecentlyPushed(string assetPath, out int index)
		{
			if (assetPath == null)
			{
				throw new ArgumentNullException(nameof(assetPath));
			}

			for (var i = _modalViews.Count - 1; i >= 0; i--)
			{
				if (string.Equals(assetPath, _modalViews[i].AssetPath))
				{
					index = i;
					return true;
				}
			}

			index = -1;
			return false;
		}

		public void DestroyRecentlyPushed(string assetPath, bool ignoreFront = true)
		{
			if (assetPath == null)
			{
				throw new ArgumentNullException(nameof(assetPath));
			}

			var frontIndex = _modalViews.Count - 1;
			if (FindIndexOfRecentlyPushed(assetPath, out var index) == false)
			{
				return;
			}

			if (ignoreFront && frontIndex == index)
			{
				return;
			}

			var modal = _modalViews[index];
			_modalViews.RemoveAt(index);

			ViewRef<ModalBackdrop>? backdrop = null;
			if (_disableBackdrop == false)
			{
				backdrop = _backdrops[index];
				_backdrops.RemoveAt(index);
			}

			DestroyAndForget(modal);
			if (backdrop.HasValue)
			{
				DestroyAndForget(backdrop.Value);
			}
		}

		public void BringToFront(ModalViewConfig config, bool ignoreFront)
		{
			BringToFrontAndForget(config, ignoreFront).Forget();
		}

		private async UniTaskVoid BringToFrontAndForget(ModalViewConfig config, bool ignoreFront)
		{
			await BringToFrontAsyncInternal(config, ignoreFront);
		}

		public async UniTask BringToFrontAsync(ModalViewConfig config, bool ignoreFront)
		{
			await BringToFrontAsyncInternal(config, ignoreFront);
		}

		public void Push<TModalView>(ModalViewConfig config) where TModalView : ModalView
		{
			PushAndForget<TModalView>(config).Forget();
		}

		private async UniTaskVoid PushAndForget<TModalView>(ModalViewConfig config) where TModalView : ModalView
		{
			await PushAsyncInternal<TModalView>(config);
		}

		public async UniTask PushAsync<TModalView>(ModalViewConfig config) where TModalView : ModalView
		{
			await PushAsyncInternal<TModalView>(config);
		}

		public void Pop(bool playAnimation)
		{
			PopAndForget(playAnimation).Forget();
		}

		private async UniTaskVoid PopAndForget(bool playAnimation)
		{
			await PopAsyncInternal(playAnimation);
		}

		public async UniTask PopAsync(bool playAnimation)
		{
			await PopAsyncInternal(playAnimation);
		}

		private async UniTask BringToFrontAsyncInternal(ModalViewConfig config, bool ignoreFront)
		{
			var assetPath = config.Config.AssetPath;
			if (assetPath == null)
			{
				throw new ArgumentNullException(nameof(assetPath));
			}

			var frontIndex = _modalViews.Count - 1;
			if (FindIndexOfRecentlyPushed(assetPath, out var index) == false)
			{
				return;
			}

			if (ignoreFront && frontIndex == index)
			{
				return;
			}

			var enterModal = _modalViews[index].View;
			enterModal.Settings = Settings;

			_modalViews.RemoveAt(index);
			RectTransform.RemoveChild(enterModal.transform);

			ViewRef<ModalBackdrop>? backdrop = null;
			if (_disableBackdrop == false)
			{
				var backdropAtIndex = _backdrops[index];
				_backdrops.RemoveAt(index);

				var backdropView = backdropAtIndex.View;
				RectTransform.RemoveChild(backdropView.transform);

				backdropView.Setup(RectTransform, config.BackdropAlpha, config.CloseWhenClickOnBackdrop);
				backdropView.Settings = Settings;

				_backdrops.Add(backdropAtIndex);
				backdrop = backdropAtIndex;
			}

			config.Config.ViewLoadedCallback?.Invoke(enterModal);

			await enterModal.AfterLoadAsync(RectTransform);

			var exitModal = _modalViews.Count == 0 ? null : _modalViews[^1].View;
			if (exitModal)
			{
				exitModal.Settings = Settings;
			}

			foreach (var callbackReceiver in _callbackReceivers)
			{
				callbackReceiver.BeforePush(enterModal, exitModal);
			}

			if (exitModal)
			{
				await exitModal.BeforeExitAsync(true);
			}

			await enterModal.BeforeEnterAsync(true);

			var animTasks = new List<UniTask>();
			
			if (backdrop.HasValue && backdrop.Value.View)
			{
				animTasks.Add(backdrop.Value.View.EnterAsync(config.Config.PlayAnimation));
			}

			if (exitModal)
			{
				animTasks.Add(exitModal.ExitAsync(true, config.Config.PlayAnimation, enterModal));
			}

			animTasks.Add(enterModal.EnterAsync(true, config.Config.PlayAnimation, exitModal));

			await UniTask.WhenAll(animTasks);
            
			_modalViews.Add(new ViewRef<ModalView>(enterModal, assetPath, config.Config.PoolingPolicy));
			IsInTransition = false;

			if (exitModal)
			{
				exitModal.AfterExit(true);
			}

			enterModal.AfterEnter(true);

			foreach (var callbackReceiver in _callbackReceivers)
			{
				callbackReceiver.AfterPush(enterModal, exitModal);
			}

			if (Settings.EnableInteractionInTransition == false)
			{
				Interactable = true;
			}
		}

		private async UniTask PushAsyncInternal<TModalView>(ModalViewConfig config) where TModalView : ModalView
		{
			var resourcePath = config.Config.AssetPath;
			if (resourcePath == null)
			{
				throw new ArgumentNullException(nameof(resourcePath));
			}

			if (IsInTransition)
			{
				Debug.LogWarning("Cannot transition because there is a modal already in transition.");
				return;
			}

			IsInTransition = true;

			if (Settings.EnableInteractionInTransition == false)
			{
				Interactable = false;
			}

			ModalBackdrop backdrop = null;

			if (_disableBackdrop == false)
			{
				var backdropAssetPath = GetBackdropResourcePath(config.ModalBackdropAssetPath);
				var backdropConfig = new ViewConfig(assetPath: backdropAssetPath, playAnimation: config.Config.PlayAnimation,
					loadAsync: config.Config.LoadAsync, poolingPolicy: PoolingPolicy.UseSettings);

				backdrop = await GetViewAsync<ModalBackdrop>(backdropConfig);
				backdrop.Setup(RectTransform, config.BackdropAlpha, config.CloseWhenClickOnBackdrop);
				_backdrops.Add(new ViewRef<ModalBackdrop>(backdrop, backdropAssetPath, backdropConfig.PoolingPolicy));
			}

			var enterModal = await GetViewAsync<TModalView>(config.Config);
			config.Config.ViewLoadedCallback?.Invoke(enterModal);

			await enterModal.AfterLoadAsync(RectTransform);

			var exitModal = _modalViews.Count == 0 ? null : _modalViews[^1].View;

			if (exitModal)
			{
				exitModal.Settings = Settings;
			}

			foreach (var callbackReceiver in _callbackReceivers)
			{
				callbackReceiver.BeforePush(enterModal, exitModal);
			}

			if (exitModal)
			{
				await exitModal.BeforeExitAsync(true);
			}

			await enterModal.BeforeEnterAsync(true);

			var animTasks = new List<UniTask>();
			
			if (backdrop)
			{
				animTasks.Add(backdrop.EnterAsync(config.Config.PlayAnimation));
			}

			if (exitModal)
			{
				animTasks.Add(exitModal.ExitAsync(true, config.Config.PlayAnimation, enterModal));
			}

			animTasks.Add(enterModal.EnterAsync(true, config.Config.PlayAnimation, exitModal));

			await UniTask.WhenAll(animTasks);
			
			_modalViews.Add(new ViewRef<ModalView>(enterModal, resourcePath, config.Config.PoolingPolicy));
			IsInTransition = false;

			if (exitModal)
			{
				exitModal.AfterExit(true);
			}

			enterModal.AfterEnter(true);

			foreach (var callbackReceiver in _callbackReceivers)
			{
				callbackReceiver.AfterPush(enterModal, exitModal);
			}

			if (Settings.EnableInteractionInTransition == false)
			{
				Interactable = true;
			}
		}

		private async UniTask PopAsyncInternal(bool playAnimation)
		{
			if (_modalViews.Count == 0)
			{
				Debug.LogError("Cannot transition because there is no modal loaded on the stack.");
				return;
			}

			if (IsInTransition)
			{
				Debug.LogWarning("Cannot transition because there is a modal already in transition.");
				return;
			}

			IsInTransition = true;

			if (Settings.EnableInteractionInTransition == false)
			{
				Interactable = false;
			}

			var lastModalIndex = _modalViews.Count - 1;
			var exitModalRef = _modalViews[lastModalIndex];
			var exitModal = exitModalRef.View;
			exitModal.Settings = Settings;

			var enterModal = _modalViews.Count == 1 ? null : _modalViews[^2].View;

			if (enterModal)
			{
				enterModal.Settings = Settings;
			}

			ViewRef<ModalBackdrop>? backdrop = null;

			if (_disableBackdrop == false)
			{
				var lastBackdropIndex = _backdrops.Count - 1;
				var lastBackdrop = _backdrops[lastBackdropIndex];
				_backdrops.RemoveAt(lastBackdropIndex);

				lastBackdrop.View.Settings = Settings;
				backdrop = lastBackdrop;
			}

			foreach (var callbackReceiver in _callbackReceivers)
			{
				callbackReceiver.BeforePop(enterModal, exitModal);
			}

			await exitModal.BeforeExitAsync(false);

			if (enterModal != null)
			{
				await enterModal.BeforeEnterAsync(false);
			}

			var animTask = new List<UniTask> { exitModal.ExitAsync(false, playAnimation, enterModal) };

			if (enterModal != null)
			{
				animTask.Add(enterModal.EnterAsync(false, playAnimation, exitModal));
			}

			if (backdrop.HasValue && backdrop.Value.View)
			{
				animTask.Add(backdrop.Value.View.ExitAsync(playAnimation));
			}

			await UniTask.WhenAll(animTask);
			
			_modalViews.RemoveAt(lastModalIndex);
			IsInTransition = false;

			exitModal.AfterExit(false);

			if (enterModal != null)
			{
				enterModal.AfterEnter(false);
			}

			foreach (var callbackReceiver in _callbackReceivers)
			{
				callbackReceiver.AfterPop(enterModal, exitModal);
			}

			await exitModal.BeforeReleaseAsync();

			DestroyAndForget(exitModalRef);

			if (backdrop.HasValue)
			{
				DestroyAndForget(backdrop.Value);
			}

			if (Settings.EnableInteractionInTransition == false)
			{
				Interactable = true;
			}
		}

		private string GetBackdropResourcePath(string assetPath)
		{
			return string.IsNullOrWhiteSpace(assetPath)
				? Settings.ModalBackdropResourcePath
				: assetPath;
		}
	}
}
