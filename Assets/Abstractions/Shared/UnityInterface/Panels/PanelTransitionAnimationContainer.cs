using System;
using UnityEngine;

namespace Assets.Abstractions.Shared.UnityInterface
{
	[Serializable]
	public class PanelTransitionAnimationContainer
	{
		[SerializeField] private TransitionAnimation _enterAnimation = new();
		[SerializeField] private TransitionAnimation _exitAnimation = new();

		public TransitionAnimation EnterAnimation => _enterAnimation;
		public TransitionAnimation ExitAnimation => _exitAnimation;

		public ITransitionAnimation GetAnimation(bool enter)
		{
			var transition = enter ? _enterAnimation : _exitAnimation;
			return transition.GetAnimation();
		}
	}
}
