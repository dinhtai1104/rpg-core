using Assets.Abstractions.Shared.Core.DI;
using Assets.Abstractions.Shared.Core.Manager;

namespace Assets.Abstractions.Shared.Core
{
	public interface IArchitecture
	{
		bool IsInitialized { get; }

		bool InjectSceneLoadedDependencies { get; }

		IInjector Injector { get; }

		IManagerInstaller ManagerInstaller { get; }

		bool HasService<TService>() where TService : IService;

		TService GetService<TService>() where TService : IService;

		void RegisterService<TService>(TService service) where TService : IService;

		void UnregisterService<TService>() where TService : IService;
	}
}
