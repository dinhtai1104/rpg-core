using Cysharp.Threading.Tasks;

namespace Assets.Abstractions.Shared.UnityInterface
{
	public abstract class PanelPresenter<TPanelView> : ViewPresenter<TPanelView>, IPanelPresenter where TPanelView : PanelView
	{
		protected PanelPresenter(IUIManager uiManager, IViewContainer viewContainer, IViewConfig viewConfig) : base(uiManager, viewContainer,
			viewConfig) { }

		UniTask IPanelLifecycleEvent.Initialize()
		{
			return ViewDidLoad(View);
		}

		public void Show(SortingLayerId? sortingLayer = null, int? orderInLayer = null)
		{
			var config = new PanelViewConfig((ViewConfig)ViewConfig, sortingLayer: sortingLayer, orderInLayer: orderInLayer);
			ViewContainer.As<PanelContainer>().Show<TPanelView>(this, config);
		}

		public UniTask ShowAsync(SortingLayerId? sortingLayer = null, int? orderInLayer = null)
		{
			var config = new PanelViewConfig((ViewConfig)ViewConfig, sortingLayer: sortingLayer, orderInLayer: orderInLayer);
			return ViewContainer.As<PanelContainer>().ShowAsync<TPanelView>(this, config);
		}

		public void Hide(bool playHideAnimation = true)
		{
			ViewContainer.As<PanelContainer>().Hide(this, playHideAnimation);
		}

		public UniTask HideAsync(bool playHideAnimation = true)
		{
			return ViewContainer.As<PanelContainer>().HideAsync(this, playHideAnimation);
		}

		public UniTask WillEnter()
		{
			return ViewWillEnter(View);
		}

		public void DidEnter()
		{
			ViewDidEnter(View);
		}

		public UniTask WillExit()
		{
			return ViewWillExit(View);
		}

		public void DidExit()
		{
			ViewDidExit(View);
		}

		public UniTask Cleanup()
		{
			return ViewWillDestroy(View);
		}

		protected virtual UniTask ViewDidLoad(TPanelView view)
		{
			return UniTask.CompletedTask;
		}

		protected virtual UniTask ViewWillEnter(TPanelView view)
		{
			return UniTask.CompletedTask;
		}

		protected virtual void ViewDidEnter(TPanelView view) { }

		protected virtual UniTask ViewWillExit(TPanelView view)
		{
			return UniTask.CompletedTask;
		}

		protected virtual void ViewDidExit(TPanelView view) { }

		protected virtual UniTask ViewWillDestroy(TPanelView view)
		{
			return UniTask.CompletedTask;
		}

		protected override void Initialize(TPanelView view)
		{
			view.AddLifecycleEvent(this, 1);
		}

		protected override void Dispose(TPanelView view)
		{
			view.RemoveLifecycleEvent(this);
		}
	}

	public abstract class PanelPresenter<TPanelView, TDataSource> : ViewPresenter<TPanelView, TDataSource>, IPanelPresenter<TDataSource>
		where TPanelView : PanelView where TDataSource : IViewDataSource
	{
		protected PanelPresenter(IUIManager uiManager, IViewContainer viewContainer, IViewConfig viewConfig) : base(uiManager, viewContainer, viewConfig) { }

		UniTask IPanelLifecycleEvent.Initialize()
		{
			return ViewDidLoad(View);
		}

		public UniTask WillEnter()
		{
			return ViewWillEnter(View);
		}

		public void DidEnter()
		{
			ViewDidEnter(View);
		}

		public UniTask WillExit()
		{
			return ViewWillExit(View);
		}

		public void DidExit()
		{
			ViewDidExit(View);
		}

		public UniTask Cleanup()
		{
			return ViewWillDestroy(View);
		}

		public void Show(TDataSource dataSource, SortingLayerId? sortingLayer = null, int? orderInLayer = null)
		{
			DataSource = dataSource;
			var config = new PanelViewConfig((ViewConfig)ViewConfig, sortingLayer: sortingLayer, orderInLayer: orderInLayer);
			ViewContainer.As<PanelContainer>().Show<TPanelView>(this, config);
		}

		public UniTask ShowAsync(TDataSource dataSource, SortingLayerId? sortingLayer = null, int? orderInLayer = null)
		{
			DataSource = dataSource;
			var config = new PanelViewConfig((ViewConfig)ViewConfig, sortingLayer: sortingLayer, orderInLayer: orderInLayer);
			return ViewContainer.As<PanelContainer>().ShowAsync<TPanelView>(this, config);
		}

		public void Hide(bool playHideAnimation = true)
		{
			ViewContainer.As<PanelContainer>().Hide(this, playHideAnimation);
		}

		public UniTask HideAsync(bool playHideAnimation = true)
		{
			return ViewContainer.As<PanelContainer>().HideAsync(this, playHideAnimation);
		}

		protected virtual UniTask ViewDidLoad(TPanelView view)
		{
			return UniTask.CompletedTask;
		}

		protected virtual UniTask ViewWillEnter(TPanelView view)
		{
			return UniTask.CompletedTask;
		}

		protected virtual void ViewDidEnter(TPanelView view) { }

		protected virtual UniTask ViewWillExit(TPanelView view)
		{
			return UniTask.CompletedTask;
		}

		protected virtual void ViewDidExit(TPanelView view) { }

		protected virtual UniTask ViewWillDestroy(TPanelView view)
		{
			return UniTask.CompletedTask;
		}

		protected override void Initialize(TPanelView view)
		{
			view.AddLifecycleEvent(this, 1);
		}

		protected override void Dispose(TPanelView view)
		{
			view.RemoveLifecycleEvent(this);
		}
	}
}
