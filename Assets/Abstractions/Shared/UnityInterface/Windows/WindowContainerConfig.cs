using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Assets.Abstractions.Shared.UnityInterface
{
	[Serializable]
	public class WindowContainerConfig
	{
		[SerializeField] private string _name;
		[SerializeField] private WindowContainerType _containerType;
		[SerializeField] private bool _overrideSorting;
		[SerializeField, ShowIf("_overrideSorting")] private SortingLayerId _sortingLayer;
		[SerializeField, ShowIf("_overrideSorting")] private int _orderInLayer = 0;

		public string Name => _name;

		public WindowContainerType ContainerType => _containerType;

		public bool OverrideSorting => _overrideSorting;

		public SortingLayerId SortingLayer => _sortingLayer;

		public int OrderInLayer => _orderInLayer;
	}
}
