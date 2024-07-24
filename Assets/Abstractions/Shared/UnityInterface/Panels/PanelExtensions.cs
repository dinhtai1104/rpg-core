using System;
using Cysharp.Threading.Tasks;

namespace Assets.Abstractions.Shared.UnityInterface
{
	public static class PanelExtensions
	{
		public static IPanelLifecycleEvent AddLifecycleEvent(this PanelView self, Func<UniTask> initialize = null, Func<UniTask> onWillShow = null,
			Action onDidShow = null, Func<UniTask> onWillHide = null, Action onDidHide = null, Func<UniTask> onCleanup = null, int priority = 0)
		{
			var lifecycleEvent = new AnonymousPanelLifecycleEvent(initialize, onWillShow, onDidShow, onWillHide, onDidHide, onCleanup);
			self.AddLifecycleEvent(lifecycleEvent, priority);
			return lifecycleEvent;
		}
	}
}
