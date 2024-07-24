using System;
using Assets.Abstractions.Shared.Core.DI;
using Assets.Abstractions.Shared.Core.Manager;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Abstractions.Shared.Core
{
	public abstract class Architecture<T> : Architecture, IArchitecture where T : Architecture<T>, new()
	{
		public static IArchitecture Instance => Lazy.Value;

		public bool IsInitialized => _initialized;
		public IInjector Injector => InjectorInternal;

		public IManagerInstaller ManagerInstaller { get; private set; }

		public abstract bool InjectSceneLoadedDependencies { get; }

		private static Lazy<T> _lazy;

		private bool _initialized;

		private static Lazy<T> Lazy
		{
			get => _lazy ??= new Lazy<T>(LazyCreate);
			set => _lazy = value;
		}

		private static T LazyCreate()
		{
			var instance = FindObjectOfType<T>(true);
			if (instance is null)
			{
				GameObject ownerObject = new GameObject($"[Architecture] {typeof(T).Name}");
				DontDestroyOnLoad(ownerObject);
				instance = ownerObject.AddComponent<T>();
				Initialize(instance);
			}

			return instance;
		}

		private static async void Initialize(T architecture)
		{
			await architecture.OnPreInitialize();
			await architecture.OnInitialize();
			await architecture.OnPostInitialize();
			architecture._initialized = true;
		}

		protected virtual UniTask OnPreInitialize()
		{
			GetAllServices(this);
			return UniTask.CompletedTask;
		}

		protected virtual async UniTask OnInitialize()
		{
			SceneManager.sceneLoaded += OnSceneLoaded;
			SceneManager.sceneUnloaded += OnSceneUnloaded;

			InjectorInternal = new Injector();
			ManagerInstaller = new ManagerInstaller(InjectorInternal);

			InjectorInternal.AddSingleton(new UnityLogger(), typeof(ILogger));
			InjectorInternal.AddSingleton(this, typeof(IArchitecture));
			InjectorInternal.AddSingleton(InjectorInternal, typeof(IInjector));
			InjectorInternal.AddSingleton(ManagerInstaller, typeof(IManagerInstaller));

			Application.wantsToQuit += OnWantsToQuit;

#if UNITY_ANDROID || UNITY_IOS
			Application.lowMemory += OnLowMemory;
#endif

			await ServiceInstaller(this);
		}

		protected virtual UniTask OnPostInitialize()
		{
			return UniTask.CompletedTask;
		}
	}
}
