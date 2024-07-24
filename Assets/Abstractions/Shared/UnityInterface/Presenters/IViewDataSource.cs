namespace Assets.Abstractions.Shared.UnityInterface
{
	public interface IViewDataSource { }

	public struct ViewDataSource<TDataSource> : IViewDataSource
	{
		public TDataSource Data { get; }

		public ViewDataSource(TDataSource data)
		{
			Data = data;
		}
	}
}
