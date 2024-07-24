namespace Assets.Abstractions.Shared.UnityInterface
{
	public readonly struct ModalViewConfig
	{
		public readonly float? BackdropAlpha;
		public readonly bool? CloseWhenClickOnBackdrop;
		public readonly string ModalBackdropAssetPath;
		public readonly ViewConfig Config;

		public ModalViewConfig(in ViewConfig config, in float? backdropAlpha = null, in bool? closeWhenClickOnBackdrop = null,
			string modalBackdropAssetPath = null)
		{
			Config = config;
			BackdropAlpha = backdropAlpha;
			CloseWhenClickOnBackdrop = closeWhenClickOnBackdrop;
			ModalBackdropAssetPath = modalBackdropAssetPath;
		}

		public ModalViewConfig(string resourcePath, bool playAnimation = true, bool loadAsync = true, in float? backdropAlpha = null,
			in bool? closeWhenClickOnBackdrop = null, string modalBackdropAssetPath = null, PoolingPolicy poolingPolicy = PoolingPolicy.UseSettings)
		{
			Config = new ViewConfig(resourcePath, playAnimation, loadAsync, poolingPolicy);
			BackdropAlpha = backdropAlpha;
			CloseWhenClickOnBackdrop = closeWhenClickOnBackdrop;
			ModalBackdropAssetPath = modalBackdropAssetPath;
		}

		public static implicit operator ModalViewConfig(in ViewConfig config) => new(config);

		public static implicit operator ModalViewConfig(string assetPath) => new(new ViewConfig(assetPath));

		public static implicit operator ViewConfig(in ModalViewConfig config) => config.Config;
	}
}
