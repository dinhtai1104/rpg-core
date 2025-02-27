using System.Collections.Generic;
using Assets.Abstractions.Shared.Core.DI;
using Assets.Abstractions.Shared.Foundation;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;

namespace Assets.Abstractions.Shared.Core
{
	public partial class Architecture
	{
		protected Injector InjectorInternal;

		private void ResolveSceneDependencies(Scene scene)
		{
			using var pooledObject = ListPool<GameObject>.Get(out var rootGameObjects);
			scene.GetRootGameObjects(rootGameObjects);

			var monoBehaviours = new List<Component>();
			foreach (var rootGameObject in rootGameObjects)
			{
				var gameObjects = rootGameObject.GetAllChildrenAndSelf();
				foreach (var go in gameObjects)
				{
					monoBehaviours.AddRange(go.GetComponents<MonoBehaviour>());
				}
			}

			foreach (var monoBehaviour in monoBehaviours)
			{
				InjectorInternal.Resolve(monoBehaviour);
			}
		}
	}
}
