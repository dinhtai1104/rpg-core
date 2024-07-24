using Assets.Abstractions.Shared.Loader.Core;
using Cysharp.Threading.Tasks;

namespace Assets.Abstractions.Shared.UnityInterface
{
	public interface IViewContainer
	{
		IAssetLoader AssetLoader { get; set; }

		bool ContainsInPool(string assetPath);

		int CountInPool(string assetPath);

		void KeepInPool(string assetPath, int amount);

		UniTask KeepInPoolAsync(string assetPath, int amount);

		void Preload(string assetPath, bool loadAsync = true, int amount = 1);

		UniTask PreloadAsync(string assetPath, bool loadAsync = true, int amount = 1);

		TView GetView<TView>(IViewConfig config) where TView : View;

		UniTask<TView> GetViewAsync<TView>(IViewConfig config) where TView : View;
	}
}
