using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Assets.Abstractions.Shared.Foundation;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Abstractions.Shared.Core
{
	public partial class Architecture
	{
		private readonly List<IInitializable> _initializables = new List<IInitializable>();
		private readonly List<IGUI> _guis = new List<IGUI>();
		private readonly List<IUpdatable> _updatables = new List<IUpdatable>();
		private readonly List<IFixedUpdatable> _fixedUpdatables = new List<IFixedUpdatable>();
		private readonly List<ILateUpdatable> _lateUpdatables = new List<ILateUpdatable>();
		private readonly List<ISceneLoad> _sceneLoads = new List<ISceneLoad>();
		private readonly List<IPausable> _pausables = new List<IPausable>();
		private readonly List<IFocusable> _focusables = new List<IFocusable>();
		private readonly List<IDestructible> _destructibles = new List<IDestructible>();
		private readonly List<IQuitable> _quitables = new List<IQuitable>();
#if UNITY_ANDROID || UNITY_IOS
		private readonly List<ILowMemory> _lowMemories = new List<ILowMemory>();
#endif
		private readonly Dictionary<Type, IService> _allServices = new Dictionary<Type, IService>();
		private IArchitecture _architecture;

		protected readonly List<Type> ServiceTypes = new List<Type>();

		protected void GetAllServices(IArchitecture architecture)
		{
			_architecture = architecture;
			var assemblies = AppDomain.CurrentDomain.GetAssemblies();
			var services = assemblies
				.SelectMany(assembly => assembly.GetTypes())
				.Where(t => t.IsClass && typeof(IService).IsAssignableFrom(t))
				.ToArray();

			foreach (var service in services)
			{
				ServiceTypes.Add(service);
			}
		}

		protected async UniTask ServiceInstaller(IArchitecture architecture)
		{
			foreach (var service in ServiceTypes)
			{
				var attribute = service.GetCustomAttribute<ServiceAttribute>();
				if (attribute != null)
				{
					var serviceType = attribute.ServiceType;
					var serviceInstance = CreateService(service);
					RegisterService(serviceType, serviceInstance);
				}
			}

			var index = 0;
			foreach (var service in _allServices.OrderBy(s => s.Value.Priority))
			{
				if (service.Value is IInitializable initializable)
				{
					Log.Info($"Initialize Service {index}: [{service.Key.Name}] => [{service.Value.GetType().Name}]");
					await initializable.OnInitialize(architecture);
					initializable.Initialized = true;
					index++;
				}
			}
		}

		private IService CreateService(Type service)
		{
			if (typeof(MonoBehaviour).IsAssignableFrom(service))
			{
				var go = new GameObject(service.Name);
				go.transform.SetParent(transform);
				return go.AddComponent(service) as IService;
			}

			if (typeof(ScriptableObject).IsAssignableFrom(service))
			{
				var obj = ScriptableObject.CreateInstance(service);
				return obj as IService;
			}

			return Activator.CreateInstance(service) as IService;
		}

		public bool HasService<TService>() where TService : IService
		{
			return _allServices.ContainsKey(typeof(TService));
		}

		public TService GetService<TService>() where TService : IService
		{
			if (_allServices.TryGetValue(typeof(TService), out var service))
			{
				return (TService)service;
			}

			return default;
		}

		public void RegisterService<TService>(TService service) where TService : IService
		{
			var type = typeof(TService);
			if (typeof(IInitializable).IsAssignableFrom(type))
			{
				var initializable = service as IInitializable;
				initializable?.OnInitialize(_architecture);
			}

			RegisterService(type, service);
		}

		public void UnregisterService<TService>() where TService : IService
		{
			var type = typeof(TService);
			if (_allServices.TryGetValue(type, out var service))
			{
				if (typeof(IInitializable).IsAssignableFrom(type))
				{
					_initializables.Remove(service as IInitializable);
				}

				if (typeof(IUpdatable).IsAssignableFrom(type))
				{
					_updatables.Remove(service as IUpdatable);
				}

				if (typeof(IFixedUpdatable).IsAssignableFrom(type))
				{
					_fixedUpdatables.Remove(service as IFixedUpdatable);
				}

				if (typeof(ILateUpdatable).IsAssignableFrom(type))
				{
					_lateUpdatables.Remove(service as ILateUpdatable);
				}

				if (typeof(IGUI).IsAssignableFrom(type))
				{
					_guis.Remove(service as IGUI);
				}

				if (typeof(ISceneLoad).IsAssignableFrom(type))
				{
					_sceneLoads.Remove(service as ISceneLoad);
				}

				if (typeof(IPausable).IsAssignableFrom(type))
				{
					_pausables.Remove(service as IPausable);
				}

				if (typeof(IFocusable).IsAssignableFrom(type))
				{
					_focusables.Remove(service as IFocusable);
				}

				if (typeof(IDestructible).IsAssignableFrom(type))
				{
					_destructibles.Remove(service as IDestructible);
				}

				if (typeof(IQuitable).IsAssignableFrom(type))
				{
					_quitables.Remove(service as IQuitable);
				}

#if UNITY_ANDROID || UNITY_IOS
				if (typeof(ILowMemory).IsAssignableFrom(type))
				{
					_lowMemories.Remove(service as ILowMemory);
				}
#endif
				_allServices.Remove(type);
			}
			else
			{
				Debug.LogError($"[{GetType().Name}] Object '{type.Name}' is not a valid service");
			}

			// if (DependencyContainer.Contains(type))
			// {
			//     DependencyContainer.Remove(type);
			// }
		}

		private void RegisterService<TService>(Type type, TService service) where TService : IService
		{
			if (typeof(IInitializable).IsAssignableFrom(type))
			{
				_initializables.Add(service as IInitializable);
			}

			if (typeof(IUpdatable).IsAssignableFrom(type))
			{
				_updatables.Add(service as IUpdatable);
			}

			if (typeof(IFixedUpdatable).IsAssignableFrom(type))
			{
				_fixedUpdatables.Add(service as IFixedUpdatable);
			}

			if (typeof(ILateUpdatable).IsAssignableFrom(type))
			{
				_lateUpdatables.Add(service as ILateUpdatable);
			}

			if (typeof(IGUI).IsAssignableFrom(type))
			{
				_guis.Add(service as IGUI);
			}

			if (typeof(ISceneLoad).IsAssignableFrom(type))
			{
				_sceneLoads.Add(service as ISceneLoad);
			}

			if (typeof(IPausable).IsAssignableFrom(type))
			{
				_pausables.Add(service as IPausable);
			}

			if (typeof(IFocusable).IsAssignableFrom(type))
			{
				_focusables.Add(service as IFocusable);
			}

			if (typeof(IDestructible).IsAssignableFrom(type))
			{
				_destructibles.Add(service as IDestructible);
			}

			if (typeof(IQuitable).IsAssignableFrom(type))
			{
				_quitables.Add(service as IQuitable);
			}

#if UNITY_ANDROID || UNITY_IOS
			if (typeof(ILowMemory).IsAssignableFrom(type))
			{
				_lowMemories.Add(service as ILowMemory);
			}
#endif

			if (typeof(IService).IsAssignableFrom(type))
			{
				_allServices.Add(type, service);
				InjectorInternal.Resolve(service);
				InjectorInternal.AddSingleton(service, type);
			}
			else
			{
				Debug.LogError($"[{GetType().Name}] Object '{type.Name}' is not a valid service");
			}
		}
	}
}
