using System.Collections.Generic;
using Assets.Abstractions.Shared.Core.DI;
using Assets.Abstractions.Shared.Core.Manager;

namespace Assets.Abstractions.Shared.UnityInterface
{
	public interface IUIManager : IManager
	{
		IInjector Injector { get; }
		
		IReadOnlyList<IWindowContainer> Containers { get; }

		IReadOnlyList<IViewPresenter> Presenters { get; }

		void AddContainer(IWindowContainer container);

		bool RemoveContainer(IWindowContainer container);

		TContainer GetContainer<TContainer>() where TContainer : IWindowContainer;

		TContainer GetContainer<TContainer>(string containerName) where TContainer : IWindowContainer;

		bool TryGetContainer<TContainer>(out TContainer container) where TContainer : IWindowContainer;

		bool TryGetContainer<TContainer>(string containerName, out TContainer container) where TContainer : IWindowContainer;

		bool HasPresenter<TPresenter>() where TPresenter : IViewPresenter;

		void AddPresenter(IViewPresenter viewPresenter);

		void RemovePresenter(IViewPresenter viewPresenter);

		TPresenter GetPresenter<TPresenter>() where TPresenter : IViewPresenter;

		bool TryGetPresenter<TPresenter>(out TPresenter viewPresenter) where TPresenter : IViewPresenter;
	}
}
