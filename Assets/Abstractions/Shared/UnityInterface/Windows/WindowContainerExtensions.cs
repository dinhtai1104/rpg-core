namespace Assets.Abstractions.Shared.UnityInterface
{
	public static class WindowContainerExtensions
	{
		public static TContainer As<TContainer>(this IViewContainer container) where TContainer : IViewContainer
		{
			return (TContainer)container;
		}
	}
}
