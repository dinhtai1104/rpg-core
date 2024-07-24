using UnityEngine;

namespace Assets.Abstractions.Shared.UnityInterface
{
	public interface IWindowContainer : IViewContainer
	{
		string ContainerName { get; }

		WindowContainerType ContainerType { get; }

		IUIManager UIManager { get; }

		Canvas Canvas { get; }
	}
}
