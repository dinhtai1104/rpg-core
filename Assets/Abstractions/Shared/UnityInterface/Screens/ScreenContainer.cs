using System;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Assets.Abstractions.Shared.UnityInterface
{
	public class ScreenContainer : WindowContainer
	{
		private readonly List<IScreenContainerCallbackReceiver> _callbackReceivers = new();

		private readonly List<ViewRef<ScreenView>> _screens = new();

		private bool _isActiveScreenStacked;

		public bool IsInTransition { get; private set; }

		public IReadOnlyList<ViewRef<ScreenView>> Screens => _screens;

		public ViewRef<ScreenView> Current => _screens[^1];

		protected override void Awake()
		{
			_callbackReceivers.AddRange(GetComponents<IScreenContainerCallbackReceiver>());
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();

			var count = _screens.Count;
			for (var i = 0; i < count; i++)
			{
				(var screen, var resourcePath) = _screens[i];
				DestroyAndForget(screen, resourcePath, PoolingPolicy.DisablePooling).Forget();
			}

			_screens.Clear();
		}

		public void AddCallbackReceiver(IScreenContainerCallbackReceiver callbackReceiver)
		{
			_callbackReceivers.Add(callbackReceiver);
		}

		public void RemoveCallbackReceiver(IScreenContainerCallbackReceiver callbackReceiver)
		{
			_callbackReceivers.Remove(callbackReceiver);
		}

		public bool FindIndexOfRecentlyPushed(string assetPath, out int index)
		{
			if (assetPath == null)
			{
				throw new ArgumentNullException(nameof(assetPath));
			}

			for (var i = _screens.Count - 1; i >= 0; i--)
			{
				if (string.Equals(assetPath, _screens[i].AssetPath))
				{
					index = i;
					return true;
				}
			}

			index = -1;
			return false;
		}

		public void BringToFront(ScreenViewConfig config, bool ignoreFront)
		{
			BringToFrontAndForget(config, ignoreFront).Forget();
		}

		private async UniTaskVoid BringToFrontAndForget(ScreenViewConfig config, bool ignoreFront)
		{
			await BringToFrontAsyncInternal(config, ignoreFront);
		}

		public async UniTask BringToFrontAsync(ScreenViewConfig config, bool ignoreFront)
		{
			await BringToFrontAsyncInternal(config, ignoreFront);
		}

		public void Push<TScreenView>(ScreenViewConfig config) where TScreenView : ScreenView
		{
			PushAndForget<TScreenView>(config).Forget();
		}

		private async UniTaskVoid PushAndForget<TScreenView>(ScreenViewConfig config) where TScreenView : ScreenView
		{
			await PushAsyncInternal<TScreenView>(config);
		}

		public async UniTask PushAsync<TScreenView>(ScreenViewConfig config) where TScreenView : ScreenView
		{
			await PushAsyncInternal<TScreenView>(config);
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

		private async UniTask BringToFrontAsyncInternal(ScreenViewConfig config, bool ignoreFront)
		{
			var assetPath = config.Config.AssetPath;
			if (assetPath == null)
			{
				throw new ArgumentNullException(nameof(assetPath));
			}

			var frontIndex = _screens.Count - 1;
			if (FindIndexOfRecentlyPushed(assetPath, out var index) == false)
			{
				return;
			}

			if (ignoreFront && frontIndex == index)
			{
				return;
			}

			var enterScreen = _screens[index].View;
			enterScreen.Settings = Settings;

			_screens.RemoveAt(index);

			RectTransform.RemoveChild(enterScreen.transform);

			config.Config.ViewLoadedCallback?.Invoke(enterScreen);

			await enterScreen.AfterLoadAsync(RectTransform);

			ViewRef<ScreenView>? exitScreenRef = _screens.Count == 0 ? null : _screens[^1];
			var exitScreen = exitScreenRef?.View;
			int? exitScreenId = null;

			if (exitScreen)
			{
				exitScreenId = exitScreen.GetInstanceID();
				exitScreen.Settings = Settings;
			}

			foreach (var callbackReceiver in _callbackReceivers)
			{
				callbackReceiver.BeforePush(enterScreen, exitScreen);
			}

			if (exitScreen)
			{
				await exitScreen.BeforeExitAsync(true);
			}

			await enterScreen.BeforeEnterAsync(true);

			var animTasks = new List<UniTask> { };
			
			if (exitScreen)
			{
				animTasks.Add(exitScreen.ExitAsync(true, config.Config.PlayAnimation, enterScreen));
			}

			animTasks.Add(enterScreen.EnterAsync(true, config.Config.PlayAnimation, exitScreen));

			await UniTask.WhenAll(animTasks);
			
			if (_isActiveScreenStacked == false && exitScreenId.HasValue)
			{
				_screens.RemoveAt(_screens.Count - 1);
			}

			_screens.Add(new ViewRef<ScreenView>(enterScreen, assetPath, config.Config.PoolingPolicy));
			IsInTransition = false;

			if (exitScreen)
			{
				exitScreen.AfterExit(true);
			}

			enterScreen.AfterEnter(true);

			foreach (var callbackReceiver in _callbackReceivers)
			{
				callbackReceiver.AfterPush(enterScreen, exitScreen);
			}

			if (_isActiveScreenStacked == false && exitScreenRef.HasValue)
			{
				await exitScreen.BeforeReleaseAsync();
				DestroyAndForget(exitScreenRef.Value);
			}

			_isActiveScreenStacked = config.Stack;

			if (Settings.EnableInteractionInTransition == false)
			{
				Interactable = true;
			}
		}

		private async UniTask PushAsyncInternal<TScreenView>(ScreenViewConfig config) where TScreenView : ScreenView
		{
			var resourcePath = config.Config.AssetPath;
			if (resourcePath == null)
			{
				throw new ArgumentNullException(nameof(resourcePath));
			}

			if (IsInTransition)
			{
				Debug.LogError($"Cannot transition because there is a screen already in transition.");
				return;
			}

			IsInTransition = true;

			if (Settings.EnableInteractionInTransition == false)
			{
				Interactable = false;
			}

			var enterScreen = await GetViewAsync<TScreenView>(config.Config);
			config.Config.ViewLoadedCallback?.Invoke(enterScreen);

			await enterScreen.AfterLoadAsync(RectTransform);

			ViewRef<ScreenView>? exitScreenRef = _screens.Count == 0 ? null : _screens[^1];
			var exitScreen = exitScreenRef?.View;
			var exitScreenId = exitScreen == null ? (int?)null : exitScreen.GetInstanceID();

			if (exitScreen)
			{
				exitScreen.Settings = Settings;
			}

			foreach (var callbackReceiver in _callbackReceivers)
			{
				callbackReceiver.BeforePush(enterScreen, exitScreen);
			}

			if (exitScreen)
			{
				await exitScreen.BeforeExitAsync(true);
			}

			await enterScreen.BeforeEnterAsync(true);

			var animTasks = new List<UniTask> { };

			if (exitScreen)
			{
				animTasks.Add(exitScreen.ExitAsync(true, config.Config.PlayAnimation, enterScreen));
			}

			animTasks.Add(enterScreen.EnterAsync(true, config.Config.PlayAnimation, exitScreen));

			await UniTask.WhenAll(animTasks);
			
			if (_isActiveScreenStacked == false && exitScreenId.HasValue)
			{
				_screens.RemoveAt(_screens.Count - 1);
			}

			_screens.Add(new ViewRef<ScreenView>(enterScreen, resourcePath, config.Config.PoolingPolicy));
			IsInTransition = false;

			if (exitScreen)
			{
				exitScreen.AfterExit(true);
			}

			enterScreen.AfterEnter(true);

			foreach (var callbackReceiver in _callbackReceivers)
			{
				callbackReceiver.AfterPush(enterScreen, exitScreen);
			}

			if (_isActiveScreenStacked == false && exitScreenRef.HasValue)
			{
				await exitScreen.BeforeReleaseAsync();
				DestroyAndForget(exitScreenRef.Value);
			}

			_isActiveScreenStacked = config.Stack;

			if (Settings.EnableInteractionInTransition == false)
			{
				Interactable = true;
			}
		}

		private async UniTask PopAsyncInternal(bool playAnimation)
		{
			if (_screens.Count == 0)
			{
				Debug.LogError("Cannot transition because there is no screen loaded on the stack.");
				return;
			}

			if (IsInTransition)
			{
				Debug.LogWarning("Cannot transition because there is a screen already in transition.");
				return;
			}

			IsInTransition = true;

			if (Settings.EnableInteractionInTransition == false)
			{
				Interactable = false;
			}

			var lastScreen = _screens.Count - 1;
			var exitScreenRef = _screens[lastScreen];
			var exitScreen = exitScreenRef.View;
			exitScreen.Settings = Settings;

			var enterScreen = _screens.Count == 1 ? null : _screens[^2].View;

			if (enterScreen)
			{
				enterScreen.Settings = Settings;
			}

			foreach (var callbackReceiver in _callbackReceivers)
			{
				callbackReceiver.BeforePop(enterScreen, exitScreen);
			}

			await exitScreen.BeforeExitAsync(false);

			if (enterScreen)
			{
				await enterScreen.BeforeEnterAsync(false);
			}

			var animTasks = new List<UniTask> { exitScreen.ExitAsync(false, playAnimation, enterScreen) };

			if (enterScreen)
			{
				animTasks.Add(enterScreen.EnterAsync(false, playAnimation, exitScreen));
			}

			await UniTask.WhenAll(animTasks);

			_screens.RemoveAt(lastScreen);
			IsInTransition = false;

			exitScreen.AfterExit(false);

			if (enterScreen)
			{
				enterScreen.AfterEnter(false);
			}

			foreach (var callbackReceiver in _callbackReceivers)
			{
				callbackReceiver.AfterPop(enterScreen, exitScreen);
			}

			await exitScreen.BeforeReleaseAsync();

			DestroyAndForget(exitScreenRef);

			_isActiveScreenStacked = true;

			if (Settings.EnableInteractionInTransition == false)
			{
				Interactable = true;
			}
		}
	}
}
