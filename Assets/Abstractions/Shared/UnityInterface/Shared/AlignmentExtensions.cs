using System;
using UnityEngine;

namespace Assets.Abstractions.Shared.UnityInterface
{
	public static class AlignmentExtensions
	{
		public static Vector3 ToPosition(this Alignment self, RectTransform rectTransform)
		{
			var rect = rectTransform.rect;
			var width = rect.width;
			var height = rect.height;
			var z = rectTransform.localPosition.z;

			switch (self)
			{
				case Alignment.Left:
					return new Vector3(-width, 0, z);
				case Alignment.Top:
					return new Vector3(0, height, z);
				case Alignment.Right:
					return new Vector3(width, 0, z);
				case Alignment.Bottom:
					return new Vector3(0, -height, z);
				case Alignment.Center:
					return new Vector3(0, 0, z);
			}

			throw new ArgumentOutOfRangeException(nameof(self), self, null);
		}
	}
}
