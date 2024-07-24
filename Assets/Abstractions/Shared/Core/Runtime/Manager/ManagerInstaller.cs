using System;
using System.Collections.Generic;
using Assets.Abstractions.Shared.Core.DI;

namespace Assets.Abstractions.Shared.Core.Manager
{
	public class ManagerInstaller : IManagerInstaller
	{
		public IEnumerable<IManager> Managers => _managers.Values;

		private readonly IInjector _injector;
		private readonly Dictionary<Type, IManager> _managers;

		public ManagerInstaller(IInjector injector)
		{
			_injector = injector;
			_managers = new Dictionary<Type, IManager>();
		}

		public IManagerInstaller Binding<TManager>(TManager instance) where TManager : IManager
		{
			_injector.AddSingleton(instance, typeof(TManager));
			_injector.Resolve(instance);
			_managers.Add(typeof(TManager), instance);
			return this;
		}

		public IManagerInstaller Binding<TManager>(Type type, TManager instance) where TManager : IManager
		{
			_injector.AddSingleton(instance, type);
			_injector.Resolve(instance);
			_managers.Add(type, instance);
			return this;
		}

		public TManager GetManager<TManager>() where TManager : IManager
		{
			foreach (var manager in _managers)
			{
				if (manager.Value is TManager typedManager)
				{
					return typedManager;
				}
			}

			return default;
		}

		public void Clear()
		{
			foreach (var kvp in _managers)
			{
				kvp.Value.Dispose();
				_injector?.Remove(kvp.Key);
			}

			_managers.Clear();
		}
	}
}
