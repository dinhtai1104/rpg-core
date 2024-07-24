namespace Assets.Abstractions.Shared.UnityInterface
{
	public readonly struct ScreenViewConfig
	{
		public readonly bool Stack;
		public readonly ViewConfig Config;

		public ScreenViewConfig(in ViewConfig options, bool stack = true)
		{
			Config = options;
			Stack = stack;
		}

		public ScreenViewConfig(string resourcePath, bool playAnimation = true, bool loadAsync = true, bool stack = true,
			PoolingPolicy poolingPolicy = PoolingPolicy.UseSettings)
		{
			Config = new ViewConfig(resourcePath, playAnimation, loadAsync, poolingPolicy);
			Stack = stack;
		}

		public static implicit operator ScreenViewConfig(in ViewConfig config) => new(config);

		public static implicit operator ScreenViewConfig(string assetPath) => new(new ViewConfig(assetPath));

		public static implicit operator ViewConfig(in ScreenViewConfig config) => config.Config;
	}
}
