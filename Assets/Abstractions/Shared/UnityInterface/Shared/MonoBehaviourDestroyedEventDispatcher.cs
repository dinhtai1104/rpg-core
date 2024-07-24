using System;
using UnityEngine;

namespace Assets.Abstractions.Shared.UnityInterface
{
	public class MonoBehaviourDestroyedEventDispatcher : MonoBehaviour
	{
		public void OnDestroy()
		{
			OnDispatch?.Invoke();
		}

		public event Action OnDispatch;
	}
}
