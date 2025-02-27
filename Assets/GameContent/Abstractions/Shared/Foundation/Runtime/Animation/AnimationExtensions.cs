using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Abstractions.Shared.Foundation.animation
{
	public static class AnimationExtensions
	{
		public static async UniTask PlayAsync(this IAnimation self, IProgress<float> progress = null)
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
