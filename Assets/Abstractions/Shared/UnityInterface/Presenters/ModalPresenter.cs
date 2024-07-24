using Cysharp.Threading.Tasks;

namespace Assets.Abstractions.Shared.UnityInterface
{
	public abstract class ModalPresenter<TModalView> : ViewPresenter<TModalView>, IModalPresenter where TModalView : ModalView
	{
		protected ModalPresenter(IUIManager uiManager, IViewContainer viewContainer, IViewConfig viewConfig) : base(uiManager, viewContainer,
			viewConfig) { }

		UniTask IModalLifecycleEvent.Initialize()
		{
			return ViewDidLoad(View);
		}

		UniTask IModalLifecycleEvent.WillPushEnter()
		{
			return ViewWillPushEnter(View);
		}

		void IModalLifecycleEvent.DidPushEnter()
		{
			ViewDidPushEnter(View);
		}

		UniTask IModalLifecycleEvent.WillPushExit()
		{
			return ViewWillPushExit(View);
		}

		void IModalLifecycleEvent.DidPushExit()
		{
			ViewDidPushExit(View);
		}

		UniTask IModalLifecycleEvent.WillPopEnter()
		{
			return ViewWillPopEnter(View);
		}

		void IModalLifecycleEvent.DidPopEnter()
		{
			ViewDidPopEnter(View);
		}

		UniTask IModalLifecycleEvent.WillPopExit()
		{
			return ViewWillPopExit(View);
		}

		void IModalLifecycleEvent.DidPopExit()
		{
			ViewDidPopExit(View);
		}

		UniTask IModalLifecycleEvent.Cleanup()
		{
			return ViewWillDestroy(View);
		}

		public void Show(int? backdropAlpha = null, bool? closeWhenClickOnBackdrop = null, string backdropAssetPath = "")
		{
			var config = new ModalViewConfig((ViewConfig)ViewConfig, backdropAlpha: backdropAlpha, closeWhenClickOnBackdrop: closeWhenClickOnBackdrop,
				modalBackdropAssetPath: backdropAssetPath);
			ViewContainer.As<ModalContainer>().Push<TModalView>(config);
		}

		public UniTask ShowAsync(int? backdropAlpha = null, bool? closeWhenClickOnBackdrop = null, string backdropAssetPath = "")
		{
			var config = new ModalViewConfig((ViewConfig)ViewConfig, backdropAlpha: backdropAlpha, closeWhenClickOnBackdrop: closeWhenClickOnBackdrop,
				modalBackdropAssetPath: backdropAssetPath);
			return ViewContainer.As<ModalContainer>().PushAsync<TModalView>(config);
		}

		public void Hide(bool playAnimation = true)
		{
			ViewContainer.As<ModalContainer>().Pop(playAnimation);
		}

		public UniTask HideAsync(bool playAnimation = true)
		{
			return ViewContainer.As<ModalContainer>().PopAsync(playAnimation);
		}

		protected virtual UniTask ViewDidLoad(TModalView view)
		{
			return UniTask.CompletedTask;
		}

		protected virtual UniTask ViewWillPushEnter(TModalView view)
		{
			return UniTask.CompletedTask;
		}

		protected virtual void ViewDidPushEnter(TModalView view) { }

		protected virtual UniTask ViewWillPushExit(TModalView view)
		{
			return UniTask.CompletedTask;
		}

		protected virtual void ViewDidPushExit(TModalView view) { }

		protected virtual UniTask ViewWillPopEnter(TModalView view)
		{
			return UniTask.CompletedTask;
		}

		protected virtual void ViewDidPopEnter(TModalView view) { }

		protected virtual UniTask ViewWillPopExit(TModalView view)
		{
			return UniTask.CompletedTask;
		}

		protected virtual void ViewDidPopExit(TModalView view) { }

		protected virtual UniTask ViewWillDestroy(TModalView view)
		{
			return UniTask.CompletedTask;
		}

		protected override void Initialize(TModalView view)
		{
			view.AddLifecycleEvent(this, 1);
		}

		protected override void Dispose(TModalView view)
		{
			view.RemoveLifecycleEvent(this);
		}
	}

	public abstract class ModalPresenter<TModalView, TDataSource> : ViewPresenter<TModalView, TDataSource>, IModalPresenter<TDataSource>
		where TModalView : ModalView where TDataSource : IViewDataSource
	{
		protected ModalPresenter(IUIManager uiManager, IViewContainer viewContainer, IViewConfig viewConfig) : base(uiManager, viewContainer, viewConfig) { }

		UniTask IModalLifecycleEvent.Initialize()
		{
			return ViewDidLoad(View);
		}

		UniTask IModalLifecycleEvent.WillPushEnter()
		{
			return ViewWillPushEnter(View);
		}

		void IModalLifecycleEvent.DidPushEnter()
		{
			ViewDidPushEnter(View);
		}

		UniTask IModalLifecycleEvent.WillPushExit()
		{
			return ViewWillPushExit(View);
		}

		void IModalLifecycleEvent.DidPushExit()
		{
			ViewDidPushExit(View);
		}

		UniTask IModalLifecycleEvent.WillPopEnter()
		{
			return ViewWillPopEnter(View);
		}

		void IModalLifecycleEvent.DidPopEnter()
		{
			ViewDidPopEnter(View);
		}

		UniTask IModalLifecycleEvent.WillPopExit()
		{
			return ViewWillPopExit(View);
		}

		void IModalLifecycleEvent.DidPopExit()
		{
			ViewDidPopExit(View);
		}

		UniTask IModalLifecycleEvent.Cleanup()
		{
			return ViewWillDestroy(View);
		}

		public void Show(TDataSource dataSource, int? backdropAlpha = null, bool? closeWhenClickOnBackdrop = null, string backdropAssetPath = "")
		{
			DataSource = dataSource;
			var config = new ModalViewConfig((ViewConfig)ViewConfig, backdropAlpha: backdropAlpha, closeWhenClickOnBackdrop: closeWhenClickOnBackdrop,
				modalBackdropAssetPath: backdropAssetPath);
			ViewContainer.As<ModalContainer>().Push<TModalView>(config);
		}

		public UniTask ShowAsync(TDataSource dataSource, int? backdropAlpha = null, bool? closeWhenClickOnBackdrop = null, string backdropAssetPath = "")
		{
			DataSource = dataSource;
			var config = new ModalViewConfig((ViewConfig)ViewConfig, backdropAlpha: backdropAlpha, closeWhenClickOnBackdrop: closeWhenClickOnBackdrop,
				modalBackdropAssetPath: backdropAssetPath);
			return ViewContainer.As<ModalContainer>().PushAsync<TModalView>(config);
		}

		public void Hide(bool playAnimation = true)
		{
			ViewContainer.As<ModalContainer>().Pop(playAnimation);
		}

		public UniTask HideAsync(bool playAnimation = true)
		{
			return ViewContainer.As<ModalContainer>().PopAsync(playAnimation);
		}

		protected virtual UniTask ViewDidLoad(TModalView view)
		{
			return UniTask.CompletedTask;
		}

		protected virtual UniTask ViewWillPushEnter(TModalView view)
		{
			return UniTask.CompletedTask;
		}

		protected virtual void ViewDidPushEnter(TModalView view) { }

		protected virtual UniTask ViewWillPushExit(TModalView view)
		{
			return UniTask.CompletedTask;
		}

		protected virtual void ViewDidPushExit(TModalView view) { }

		protected virtual UniTask ViewWillPopEnter(TModalView view)
		{
			return UniTask.CompletedTask;
		}

		protected virtual void ViewDidPopEnter(TModalView view) { }

		protected virtual UniTask ViewWillPopExit(TModalView view)
		{
			return UniTask.CompletedTask;
		}

		protected virtual void ViewDidPopExit(TModalView view) { }

		protected virtual UniTask ViewWillDestroy(TModalView view)
		{
			return UniTask.CompletedTask;
		}

		protected override void Initialize(TModalView view)
		{
			view.AddLifecycleEvent(this, 1);
		}

		protected override void Dispose(TModalView view)
		{
			view.RemoveLifecycleEvent(this);
		}
	}
}
