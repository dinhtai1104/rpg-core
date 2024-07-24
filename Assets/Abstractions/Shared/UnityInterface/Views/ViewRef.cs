namespace Assets.Abstractions.Shared.UnityInterface
{
	public readonly struct ViewRef<T> where T : View
	{
		public readonly PoolingPolicy PoolingPolicy;
		public readonly T View;
		public readonly string AssetPath;

		public ViewRef(T view, string assetPath, PoolingPolicy poolingPolicy)
		{
			PoolingPolicy = poolingPolicy;
			View = view;
			AssetPath = assetPath;
		}

		public void Deconstruct(out T view, out string assetPath)
		{
			view = View;
			assetPath = AssetPath;
		}

		public void Deconstruct(out T view, out string assetPath, out PoolingPolicy poolingPolicy)
		{
			view = View;
			assetPath = AssetPath;
			poolingPolicy = PoolingPolicy;
		}

		public static implicit operator ViewRef(ViewRef<T> value) => new ViewRef(value.View, value.AssetPath, value.PoolingPolicy);
	}

	public readonly struct ViewRef
	{
		public readonly PoolingPolicy PoolingPolicy;
		public readonly View View;
		public readonly string AssetPath;

		public ViewRef(View view, string assetPath, PoolingPolicy poolingPolicy)
		{
			PoolingPolicy = poolingPolicy;
			View = view;
			AssetPath = assetPath;
		}

		public void Deconstruct(out View view, out string assetPath)
		{
			view = View;
			assetPath = AssetPath;
		}

		public void Deconstruct(out View view, out string assetPath, out PoolingPolicy poolingPolicy)
		{
			view = View;
			assetPath = AssetPath;
			poolingPolicy = PoolingPolicy;
		}
	}
}
