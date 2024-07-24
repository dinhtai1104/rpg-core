using System;
using Cysharp.Threading.Tasks;

namespace Assets.Abstractions.Shared.UnityInterface
{
	public static class ScreenExtensions
	{
		public static void AddLifecycleEvent(this ScreenView self, Func<UniTask> initialize = null, Func<UniTask> onWillPushEnter = null,
			Action onDidPushEnter = null, Func<UniTask> onWillPushExit = null, Action onDidPushExit = null, Func<UniTask> onWillPopEnter = null,
			Action onDidPopEnter = null, Func<UniTask> onWillPopExit = null, Action onDidPopExit = null, Func<UniTask> onCleanup = null, int priority = 0)
		{
			var lifecycleEvent = new AnonymousScreenLifecycleEvent(
				initialize,
				onWillPushEnter, onDidPushEnter,
				onWillPushExit, onDidPushExit,
				onWillPopEnter, onDidPopEnter,
				onWillPopExit, onDidPopExit,
				onCleanup
			);

			self.AddLifecycleEvent(lifecycleEvent, priority);
		}
	}
}
