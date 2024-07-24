using UnityEngine;

namespace Assets.Abstractions.Shared.UnityInterface
{
	internal static class TransformExtensions
	{
		public static bool TryGetTransform<T>(this T layer, out Transform transform)
		{
			transform = null;

			if (layer is View view)
			{
				transform = view.RectTransform;
			}
			else if (layer is Component component)
			{
				transform = component.transform;
			}
			else if (layer is ITransform hasTransform)
			{
				transform = hasTransform.Transform;
			}

			return transform;
		}
	}
}
