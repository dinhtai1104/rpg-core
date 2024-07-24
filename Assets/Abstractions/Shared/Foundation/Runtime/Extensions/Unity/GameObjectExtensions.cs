using System.Collections.Generic;
using UnityEngine;

namespace Assets.Abstractions.Shared.Foundation
{
	/// <summary> GameObject extensions. </summary>
	public static class GameObjectExtension
	{
		public static T GetOrAddComponent<T>(this GameObject self)
			where T : Component
		{
			if (self.TryGetComponent<T>(out var component) == false)
			{
				component = self.AddComponent<T>();
			}

			return component;
		}

		public static T GetOrAddComponent<T>(this Component self)
			where T : Component
		{
			if (self.TryGetComponent<T>(out var component) == false)
			{
				component = self.gameObject.AddComponent<T>();
			}

			return component;
		}

		public static bool TryGetOrAddComponent<T>(this GameObject self, out T component)
			where T : Component
		{
			if (self.TryGetComponent<T>(out component) == false)
			{
				component = self.gameObject.AddComponent<T>();
			}

			return component;
		}

		public static bool TryGetOrAddComponent<T>(this Component self, out T component)
			where T : Component
		{
			if (self.TryGetComponent<T>(out component) == false)
			{
				component = self.gameObject.AddComponent<T>();
			}

			return component;
		}

		/// <summary> Returns a component of the parent of a GameObject, ignoring itself. </summary>
		/// <returns>Component</returns>
		public static T GetComponentInParentIgnoreSelf<T>(this GameObject self) => self.transform.parent.GetComponentInParent<T>();

		/// <summary> Return a list of all child objects. </summary>
		/// <returns></returns>
		public static List<GameObject> GetAllChildren(this GameObject self)
		{
			Transform[] childTransforms = self.GetComponentsInChildren<Transform>();
			List<GameObject> allChildren = new List<GameObject>(childTransforms.Length);

			foreach (Transform child in childTransforms)
			{
				if (child.gameObject != self)
					allChildren.Add(child.gameObject);
			}

			return allChildren;
		}

		/// <summary> Return a list of all child objects including itself. </summary>
		/// <returns>List.</returns>
		public static List<GameObject> GetAllChildrenAndSelf(this GameObject self)
		{
			Transform[] childTransforms = self.GetComponentsInChildren<Transform>();
			List<GameObject> allChildren = new List<GameObject>(childTransforms.Length);

			for (int transformIndex = 0; transformIndex < childTransforms.Length; ++transformIndex)
				allChildren.Add(childTransforms[transformIndex].gameObject);

			return allChildren;
		}

		/// <summary> Changes all the materials of a GameObject. </summary>
		/// <param name="go">GameObject</param>
		/// <param name="newMaterial">Material</param>
		public static void ChangeMaterial(this GameObject go, Material newMaterial)
		{
			Renderer[] children = go.GetComponentsInChildren<Renderer>(true);
			for (int i = 0; i < children.Length; ++i)
			{
				Renderer rend = children[i];

				Material[] mats = new Material[rend.materials.Length];
				for (int j = 0; j < rend.materials.Length; j++)
					mats[j] = newMaterial;

				rend.materials = mats;
			}
		}

		/// <summary> Destroy all children. </summary>
		public static void DestroyAllChildren(this Transform self)
		{
			for (var i = self.childCount - 1; i > -1; i--)
				SafeDestroy(self.GetChild(i).gameObject);
		}

		/// <summary> Destroy Object. </summary>
		public static void SafeDestroy<T>(this T self) where T : Object
		{
#if UNITY_EDITOR
			if (Application.isEditor == true)
				Object.DestroyImmediate(self);
			else
#endif
				Object.Destroy(self);
		}

		/// <summary> Destroy GameObject. </summary>
		public static void SafeDestroy(this GameObject self)
		{
#if UNITY_EDITOR
			if (Application.isEditor == true)
				Object.DestroyImmediate(self);
			else
#endif
				Object.Destroy(self);
		}

		/// <summary> Destroy component. </summary>
		public static void SafeDestroyComponent<T>(this T self) where T : Component
		{
			if (self != null)
				SafeDestroy(self.gameObject);
		}

		/// <summary> Returns the first child GameObject with a name, or null. </summary>
		/// <param name="name">Name</param>
		/// <returns>GameObject or null</returns>
		public static GameObject FindChildrenByName(string name)
		{
			GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();
			for (int i = 0; i < allObjects.Length; ++i)
			{
				if (name.Equals(allObjects[i].name) == true)
					return allObjects[i];
			}

			return null;
		}
	}
}
