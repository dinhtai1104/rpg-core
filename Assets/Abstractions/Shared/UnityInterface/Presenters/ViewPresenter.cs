using System;
using Assets.Abstractions.Shared.UnRegister;

namespace Assets.Abstractions.Shared.UnityInterface
{
	public abstract class ViewPresenter<TView> : IViewPresenter, IUnRegister where TView : IView
	{
#region Builder
		public class Builder<TPresenter> where TPresenter : ViewPresenter<TView>
		{
			private readonly IUIManager _uiManager;
			private readonly string _assetPath;
			private PoolingPolicy _poolingPolicy = PoolingPolicy.UseSettings;
			private bool _playAnimation = true;
			private bool _loadAsync = true;
			private IViewContainer _viewContainer;

			public Builder(IUIManager uiManager, string assetPath)
			{
				_uiManager = uiManager;
				_assetPath = assetPath;
			}

			public Builder<TPresenter> WithContainer(string containerName)
			{
				if (_uiManager.TryGetContainer<IWindowContainer>(containerName, out var container))
				{
					_viewContainer = container;
				}

				return this;
			}

			public Builder<TPresenter> WithPoolingPolicy(PoolingPolicy poolingPolicy)
			{
				_poolingPolicy = poolingPolicy;
				return this;
			}

			public Builder<TPresenter> WithPlayAnimation(bool playAnimation)
			{
				_playAnimation = playAnimation;
				return this;
			}

			public Builder<TPresenter> WithLoadAsync(bool loadAsync)
			{
				_loadAsync = loadAsync;
				return this;
			}

			public TPresenter Build()
			{
				if (_viewContainer == null)
				{
					throw new InvalidOperationException("WithContainer must be called before Build.");
				}

				var viewConfig = new ViewConfig(_assetPath, _playAnimation, _loadAsync, _poolingPolicy);
				var viewPresenter = (TPresenter)Activator.CreateInstance(typeof(TPresenter), _uiManager, _viewContainer, viewConfig);
				_uiManager.Injector?.Resolve(viewPresenter);
				return viewPresenter;
			}
		}
#endregion

		public bool IsDisposed { get; private set; }

		public bool IsInitialized { get; private set; }

		public IViewContainer ViewContainer { get; private set; }

		public IViewConfig ViewConfig { get; private set; }

		public TView View { get; private set; }

		protected IUIManager UIManager { get; private set; }

		protected ViewPresenter(IUIManager uiManager, IViewContainer viewContainer, IViewConfig viewConfig)
		{
			UIManager = uiManager;
			ViewContainer = viewContainer;
			ViewConfig = viewConfig;
			ViewConfig.SetViewLoadedCallback(OnViewLoaded);
			Initialize();
		}

		private void Initialize()
		{
			if (IsInitialized)
			{
				throw new InvalidOperationException($"{GetType().Name} is already initialized.");
			}

			if (IsDisposed)
			{
				throw new ObjectDisposedException(nameof(ViewPresenter<TView>));
			}

			if (UIManager == null)
			{
				throw new ArgumentNullException(nameof(UIManager));
			}

			if (ViewContainer == null)
			{
				throw new ArgumentNullException(nameof(ViewContainer));
			}

			if (ViewConfig == null)
			{
				throw new ArgumentNullException(nameof(ViewConfig));
			}

			if (ViewConfig.PoolingPolicy == PoolingPolicy.UseSettings || ViewConfig.PoolingPolicy == PoolingPolicy.EnablePooling)
			{
				ViewContainer.Preload(ViewConfig.AssetPath);
			}

			UIManager.AddPresenter(this);
			IsInitialized = true;
		}

		public virtual void Dispose()
		{
			if (!IsInitialized)
			{
				return;
			}

			if (IsDisposed)
			{
				return;
			}

			ViewConfig.SetViewLoadedCallback(null);
			IsDisposed = true;
		}

		public void UnRegister()
		{
			if (View != null)
			{
				Dispose(View);
			}
		}

		protected abstract void Initialize(TView view);

		protected abstract void Dispose(TView view);

		private void OnViewLoaded(IView view)
		{
			View = (TView)view;
			Initialize(View);
			this.UnRegisterWhenGameObjectDestroyed(View.Owner);
		}
	}

	public abstract class ViewPresenter<TView, TDataSource> : ViewPresenter<TView> where TView : IView where TDataSource : IViewDataSource
	{
		protected TDataSource DataSource { get; set; }

		protected ViewPresenter(IUIManager uiManager, IViewContainer viewContainer, IViewConfig viewConfig)
			: base(uiManager, viewContainer, viewConfig) { }
	}
}
