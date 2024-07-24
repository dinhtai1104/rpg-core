using System;
using Cysharp.Threading.Tasks;
using Assets.Abstractions.Shared.Foundation.animation;
using UnityEngine;

namespace Assets.Abstractions.Shared.UnityInterface
{
	internal static class TransitionAnimationExtensions
	{
		public static async UniTask PlayAsync(this ITransitionAnimation self, IProgress<float> progress = null)
		{
			var player = new AnimationPlayer(self);
			progress?.Report(0.0f);
			player.Play();

			while (player.IsFinished == false)
			{
				await UniTask.NextFrame();
				player.Update(Time.unscaledDeltaTime);
				progress?.Report(player.Time / self.Duration);
			}
		}
	}
}
