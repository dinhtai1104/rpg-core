using UnityEngine;

namespace Assets.Abstractions.Shared.UnityInterface
{
	public abstract class TransitionAnimationBehaviour : MonoBehaviour, ITransitionAnimation
	{
		public RectTransform RectTransform { get; private set; }

		public RectTransform PartnerRectTransform { get; private set; }

		public abstract float Duration { get; }

		void ITransitionAnimation.SetPartner(RectTransform partnerRectTransform)
		{
			PartnerRectTransform = partnerRectTransform;
		}

		void ITransitionAnimation.Setup(RectTransform rectTransform)
		{
			RectTransform = rectTransform;
			Setup();
			SetTime(0.0f);
		}

		public abstract void Setup();

		public abstract void SetTime(float time);
	}
}
