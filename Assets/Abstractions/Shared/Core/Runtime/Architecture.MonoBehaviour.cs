using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Abstractions.Shared.Core
{
	[DisallowMultipleComponent]
	public partial class Architecture : MonoBehaviour
	{
		private bool WillDestroy { get; set; }

		protected virtual void OnUpdate() { }

		protected virtual void OnFixedUpdate() { }

		protected virtual void OnLateUpdate() { }

		protected virtual void OnWillDestroy() { }

		private void Update()
		{
			if (_architecture == null)
			{
				return;
			}

			if (_architecture.IsInitialized)
			{
				if (!WillDestroy)
				{
					OnUpdate();

					foreach (var updatable in _updatables)
					{
						updatable.OnUpdate();
					}
				}
			}
		}

		private void FixedUpdate()
		{
			if (_architecture == null)
			{
				return;
			}

			if (_architecture.IsInitialized)
			{
				if (!WillDestroy)
				{
					OnFixedUpdate();

					foreach (var fixedUpdatable in _fixedUpdatables)
					{
						fixedUpdatable.OnFixedUpdate();
					}
				}
			}
		}

		private void LateUpdate()
		{
			if (_architecture == null)
			{
				return;
			}

			if (_architecture.IsInitialized)
			{
				if (!WillDestroy)
				{
					OnLateUpdate();

					foreach (var lateUpdatable in _lateUpdatables)
					{
						lateUpdatable.OnLateUpdate();
					}
				}
			}
		}

		private void OnDestroy()
		{
			SceneManager.sceneLoaded -= OnSceneLoaded;
			SceneManager.sceneUnloaded -= OnSceneUnloaded;

			WillDestroy = true;
			foreach (var destructible in _destructibles)
			{
				destructible.WillDestroy = true;
				destructible.OnWillDestroy();
			}

			OnWillDestroy();

			_initializables.Clear();
			_guis.Clear();
			_updatables.Clear();
			_fixedUpdatables.Clear();
			_lateUpdatables.Clear();
			_destructibles.Clear();
			_sceneLoads.Clear();
			_pausables.Clear();
			_focusables.Clear();
			_quitables.Clear();
#if UNITY_ANDROID || UNITY_IOS
			_lowMemories.Clear();
#endif
			_allServices.Clear();
			InjectorInternal.Dispose();

#if UNITY_ANDROID || UNITY_IOS
			Application.lowMemory -= OnLowMemory;
#endif
			Application.wantsToQuit -= OnWantsToQuit;
		}

#if UNITY_EDITOR
		private void OnDrawGizmos()
		{
			foreach (var gui in _guis)
			{
				gui.OnGizmos();
			}
		}
#endif

		private void OnGUI()
		{
			foreach (var gui in _guis)
			{
				gui.OnGUI();
			}
		}

		private void OnApplicationPause(bool pause)
		{
			foreach (var pausable in _pausables)
			{
				pausable.OnAppPause(pause);
			}
		}

		private void OnApplicationFocus(bool focus)
		{
			foreach (var focusable in _focusables)
			{
				focusable.OnAppFocus(focus);
			}
		}

		private void OnApplicationQuit()
		{
			WillDestroy = true;
			foreach (var destructible in _destructibles)
			{
				destructible.WillDestroy = true;
			}

			foreach (var quitable in _quitables)
			{
				quitable.OnAppQuit();
			}
		}

		protected void OnSceneLoaded(Scene current, LoadSceneMode mode)
		{
			foreach (var sceneLoad in _sceneLoads)
			{
				sceneLoad.OnSceneLoad(current.name);
			}

			if (_architecture.InjectSceneLoadedDependencies)
			{
				ResolveSceneDependencies(current);
			}
		}

		protected void OnSceneUnloaded(Scene current)
		{
			foreach (var sceneLoad in _sceneLoads)
			{
				sceneLoad.OnSceneUnload(current.name);
			}
		}

		protected bool OnWantsToQuit()
		{
			foreach (var initializable in _initializables)
			{
				if (initializable.Initialized)
				{
					initializable.Initialized = false;
				}
			}

			WillDestroy = true;
			return true;
		}

#if UNITY_ANDROID || UNITY_IOS
		protected void OnLowMemory()
		{
			foreach (var lowMemory in _lowMemories)
			{
				lowMemory.OnLowMemory();
			}
		}
#endif
	}
}
