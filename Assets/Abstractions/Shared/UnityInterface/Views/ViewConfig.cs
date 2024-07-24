using System;

namespace Assets.Abstractions.Shared.UnityInterface
{
	public struct ViewConfig : IViewConfig
	{
		public bool LoadAsync { get; }
		public bool PlayAnimation { get; }
		public string AssetPath { get; }
		public PoolingPolicy PoolingPolicy { get; }
		public Action<IView> ViewLoadedCallback { get; private set; }

		public void SetViewLoadedCallback(Action<IView> callback)
		{
			ViewLoadedCallback = callback;
		}

		public ViewConfig(string assetPath, bool playAnimation = true, bool loadAsync = true, PoolingPolicy poolingPolicy = PoolingPolicy.UseSettings)
		{
			LoadAsync = loadAsync;
			PlayAnimation = playAnimation;
			PoolingPolicy = poolingPolicy;
			AssetPath = assetPath;
			ViewLoadedCallback = null;
		}

		public static implicit operator ViewConfig(string assetPath) => new(assetPath);
	}
}
