using System;

namespace Assets.Abstractions.Shared.UnityInterface
{
	public readonly struct PanelViewConfig
	{
		public readonly SortingLayerId? SortingLayer;
		public readonly int? OrderInLayer;
		public readonly ViewConfig Config;

		public PanelViewConfig(in ViewConfig options, in SortingLayerId? sortingLayer = null, in int? orderInLayer = null)
		{
			Config = options;
			SortingLayer = sortingLayer;
			OrderInLayer = orderInLayer;
		}

		public PanelViewConfig(string resourcePath
			, bool playAnimation = true
			, bool loadAsync = true
			, in SortingLayerId? sortingLayer = null
			, in int? orderInLayer = null
			, PoolingPolicy poolingPolicy = PoolingPolicy.UseSettings)
		{
			Config = new ViewConfig(resourcePath, playAnimation, loadAsync, poolingPolicy);
			SortingLayer = sortingLayer;
			OrderInLayer = orderInLayer;
		}

		public static implicit operator PanelViewConfig(in ViewConfig config) => new(config);

		public static implicit operator PanelViewConfig(string assetPath) => new(new ViewConfig(assetPath));

		public static implicit operator ViewConfig(in PanelViewConfig config) => config.Config;
	}
}
