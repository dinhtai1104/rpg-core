using Assets.Abstractions.Shared.Loader.Core;
using Sirenix.OdinInspector;
using UnityEngine;

#if USE_ADDRESSABLES
using Assets.Abstractions.Shared.Loader.Addressable;
#else
using Assets.Abstractions.Shared.Loader.Resource;
#endif

namespace Assets.Abstractions.Shared.UnityInterface
{
	[CreateAssetMenu(fileName = "UISettings", menuName = "Sparkle/UI/UI Settings", order = 0)]
	public class UISettings : ScriptableObject
	{
		private const string DefaultModalBackdropPrefabKey = "DefaultModalBackdrop";
		[SerializeField] private TransitionAnimationObject _screenPushEnterAnimation;
		[SerializeField] private TransitionAnimationObject _screenPushExitAnimation;
		[SerializeField] private TransitionAnimationObject _screenPopEnterAnimation;
		[SerializeField] private TransitionAnimationObject _screenPopExitAnimation;
		[SerializeField] private TransitionAnimationObject _modalEnterAnimation;
		[SerializeField] private TransitionAnimationObject _modalExitAnimation;
		[SerializeField] private TransitionAnimationObject _modalBackdropEnterAnimation;
		[SerializeField] private TransitionAnimationObject _modalBackdropExitAnimation;
		[SerializeField] private TransitionAnimationObject _panelEnterAnimation;
		[SerializeField] private TransitionAnimationObject _panelExitAnimation;
		[SerializeField] private AssetLoaderObject _assetLoader;
		[SerializeField] private bool _enablePooling;
		[SerializeField] private bool _enableInteractionInTransition;
		[SerializeField] private bool _disableModalBackdrop;
		[SerializeField, HideIf("_disableModalBackdrop")] private string _modalBackdropResourcePath = DefaultModalBackdropPrefabKey;

		private IAssetLoader _defaultAssetLoader;

		private ITransitionAnimation ScreenPushEnterAnimation =>
			_screenPushEnterAnimation
				? Instantiate(_screenPushEnterAnimation)
				: SimpleTransitionAnimationObject.CreateInstance(beforeAlignment: Alignment.Right, afterAlignment: Alignment.Center);

		private ITransitionAnimation ScreenPushExitAnimation =>
			_screenPushExitAnimation
				? Instantiate(_screenPushExitAnimation)
				: SimpleTransitionAnimationObject.CreateInstance(beforeAlignment: Alignment.Center, afterAlignment: Alignment.Left);

		private ITransitionAnimation ScreenPopEnterAnimation =>
			_screenPopEnterAnimation
				? Instantiate(_screenPopEnterAnimation)
				: SimpleTransitionAnimationObject.CreateInstance(beforeAlignment: Alignment.Left, afterAlignment: Alignment.Center);

		private ITransitionAnimation ScreenPopExitAnimation =>
			_screenPopExitAnimation
				? Instantiate(_screenPopExitAnimation)
				: SimpleTransitionAnimationObject.CreateInstance(beforeAlignment: Alignment.Center, afterAlignment: Alignment.Right);

		private ITransitionAnimation ModalEnterAnimation =>
			_modalEnterAnimation
				? Instantiate(_modalEnterAnimation)
				: SimpleTransitionAnimationObject.CreateInstance(beforeScale: Vector3.one * 0.3f, beforeAlpha: 0.0f);

		private ITransitionAnimation ModalExitAnimation =>
			_modalExitAnimation
				? Instantiate(_modalExitAnimation)
				: SimpleTransitionAnimationObject.CreateInstance(afterScale: Vector3.one * 0.3f, afterAlpha: 0.0f);

		private ITransitionAnimation ModalBackdropEnterAnimation =>
			_modalBackdropEnterAnimation
				? Instantiate(_modalBackdropEnterAnimation)
				: SimpleTransitionAnimationObject.CreateInstance(beforeAlpha: 0.0f, easeType: EaseType.Linear);

		private ITransitionAnimation ModalBackdropExitAnimation =>
			_modalBackdropExitAnimation
				? Instantiate(_modalBackdropExitAnimation)
				: SimpleTransitionAnimationObject.CreateInstance(afterAlpha: 0.0f, easeType: EaseType.Linear);

		private ITransitionAnimation PanelEnterAnimation =>
			_panelEnterAnimation
				? Instantiate(_panelEnterAnimation)
				: SimpleTransitionAnimationObject.CreateInstance(beforeScale: Vector3.one * 0.3f, beforeAlpha: 0.0f);

		private ITransitionAnimation PanelExitAnimation =>
			_panelExitAnimation
				? Instantiate(_panelExitAnimation)
				: SimpleTransitionAnimationObject.CreateInstance(afterScale: Vector3.one * 0.3f, afterAlpha: 0.0f);

		public string ModalBackdropResourcePath =>
			string.IsNullOrWhiteSpace(_modalBackdropResourcePath)
				? DefaultModalBackdropPrefabKey
				: _modalBackdropResourcePath;

		public IAssetLoader AssetLoader
		{
			get
			{
				if (_assetLoader)
				{
					return _assetLoader;
				}

				if (_defaultAssetLoader == null)
				{
#if USE_ADDRESSABLES
                    _defaultAssetLoader = CreateInstance<AddressableAssetLoaderObject>();
#else
					_defaultAssetLoader = CreateInstance<ResourcesAssetLoaderObject>();
#endif
				}

				return _defaultAssetLoader;
			}
		}

		public bool EnablePooling => _enablePooling;

		public bool EnableInteractionInTransition => _enableInteractionInTransition;

		public bool DisableModalBackdrop => _disableModalBackdrop;

		public ITransitionAnimation GetDefaultScreenTransitionAnimation(bool push, bool enter)
		{
			if (push)
			{
				return enter ? ScreenPushEnterAnimation : ScreenPushExitAnimation;
			}

			return enter ? ScreenPopEnterAnimation : ScreenPopExitAnimation;
		}

		public ITransitionAnimation GetDefaultModalTransitionAnimation(bool enter)
		{
			return enter ? ModalEnterAnimation : ModalExitAnimation;
		}

		public ITransitionAnimation GetDefaultModalBackdropTransitionAnimation(bool enter)
		{
			return enter ? ModalBackdropEnterAnimation : ModalBackdropExitAnimation;
		}

		public ITransitionAnimation GetDefaultPanelTransitionAnimation(bool enter)
		{
			return enter ? PanelEnterAnimation : PanelExitAnimation;
		}
	}
}
