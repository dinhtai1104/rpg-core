using UnityEngine;

namespace Assets.Abstractions.Shared.UnityInterface
{
	[CreateAssetMenu(fileName = "WindowContainerSettings", menuName = "Sparkle/UI/Window Container Settings", order = 1)]
	public class WindowContainerSettings : ScriptableObject
	{
		[SerializeField] private WindowContainerConfig[] _containers;

		public WindowContainerConfig[] Containers => _containers;
	}
}
