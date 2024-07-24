using Cysharp.Threading.Tasks;

namespace Assets.Abstractions.Shared.UnityInterface
{
	public interface IModalPresenter : IViewPresenter, IModalLifecycleEvent
	{
		void Show(int? backdropAlpha = null, bool? closeWhenClickOnBackdrop = null, string backdropAssetPath = "");

		UniTask ShowAsync(int? backdropAlpha = null, bool? closeWhenClickOnBackdrop = null, string backdropAssetPath = "");

		void Hide(bool playAnimation = true);

		UniTask HideAsync(bool playAnimation = true);
	}

	public interface IModalPresenter<in TDataSource> : IViewPresenter, IModalLifecycleEvent where TDataSource : IViewDataSource
	{
		void Show(TDataSource dataSource, int? backdropAlpha = null, bool? closeWhenClickOnBackdrop = null, string backdropAssetPath = "");

		UniTask ShowAsync(TDataSource dataSource, int? backdropAlpha = null, bool? closeWhenClickOnBackdrop = null, string backdropAssetPath = "");

		void Hide(bool playAnimation = true);

		UniTask HideAsync(bool playAnimation = true);
	}
}
