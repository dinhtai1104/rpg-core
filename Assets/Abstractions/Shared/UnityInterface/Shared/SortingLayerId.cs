using System;
using UnityEngine;

namespace Assets.Abstractions.Shared.UnityInterface
{
	[Serializable]
	public struct SortingLayerId : IEquatable<SortingLayerId>
	{
		public int ID;

		public int Layer => SortingLayer.GetLayerValueFromID(ID);

		public string Name => SortingLayer.IDToName(ID);

		public SortingLayerId(int value)
		{
			ID = value;
		}

		public SortingLayerId(string name)
		{
			ID = SortingLayer.NameToID(name);
		}

		public override int GetHashCode() => ID.GetHashCode();

		public override bool Equals(object obj) => obj is SortingLayerId other && ID == other.ID;

		public bool Equals(SortingLayerId other) => ID == other.ID;

		public override string ToString() => ID.ToString();

		public static implicit operator int(SortingLayerId value) => value.ID;

		public static implicit operator SortingLayerId(int value) => new(value);

		public static implicit operator SortingLayerId(string value) => new(value);

		public static bool operator ==(SortingLayerId lhs, SortingLayerId rhs) => lhs.ID == rhs.ID;

		public static bool operator !=(SortingLayerId lhs, SortingLayerId rhs) => lhs.ID != rhs.ID;
	}
}
