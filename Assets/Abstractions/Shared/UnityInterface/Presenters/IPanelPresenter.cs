using Cysharp.Threading.Tasks;

namespace Assets.Abstractions.Shared.UnityInterface
{
	public interface IPanelPresenter : IViewPresenter, IPanelLifecycleEvent
	{
		void Show(SortingLayerId? sortingLayer, int? orderInLayer);

		UniTask ShowAsync(SortingLayerId? sortingLayer, int? orderInLayer);

		void Hide(bool playHideAnimation = true);

		UniTask HideAsync(bool playHideAnimation = true);
	}

	public interface IPanelPresenter<in TDataSource> : IViewPresenter, IPanelLifecycleEvent where TDataSource : IViewDataSource
	{
		void Show(TDataSource dataSource, SortingLayerId? sortingLayer, int? orderInLayer);

		UniTask ShowAsync(TDataSource dataSource, SortingLayerId? sortingLayer, int? orderInLayer);

		void Hide(bool playHideAnimation = true);

		UniTask HideAsync(bool playHideAnimation = true);
	}
}
