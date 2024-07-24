using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace Assets.Abstractions.Shared.UnityInterface
{
	public class AnonymousPanelLifecycleEvent : IPanelLifecycleEvent
	{
		public event Action OnDidEnter;

		public event Action OnDidExit;

		public AnonymousPanelLifecycleEvent(Func<UniTask> initialize = null, Func<UniTask> onWillEnter = null, Action onDidEnter = null,
			Func<UniTask> onWillExit = null, Action onDidExit = null, Func<UniTask> onCleanup = null)
		{
			if (initialize != null)
			{
				OnInitialize.Add(initialize);
			}

			if (onWillEnter != null)
			{
				OnWillEnter.Add(onWillEnter);
			}

			OnDidEnter = onDidEnter;

			if (onWillExit != null)
			{
				OnWillExit.Add(onWillExit);
			}

			OnDidExit = onDidExit;

			if (onCleanup != null)
			{
				OnCleanup.Add(onCleanup);
			}
		}

		public List<Func<UniTask>> OnInitialize { get; } = new();

		public List<Func<UniTask>> OnWillEnter { get; } = new();

		public List<Func<UniTask>> OnWillExit { get; } = new();

		public List<Func<UniTask>> OnCleanup { get; } = new();

		async UniTask IPanelLifecycleEvent.Initialize()
		{
			foreach (var onInitialize in OnInitialize)
			{
				await onInitialize.Invoke();
			}
		}

		async UniTask IPanelLifecycleEvent.WillEnter()
		{
			foreach (var onWillShowEnter in OnWillEnter)
			{
				await onWillShowEnter.Invoke();
			}
		}

		void IPanelLifecycleEvent.DidEnter()
		{
			OnDidEnter?.Invoke();
		}

		async UniTask IPanelLifecycleEvent.WillExit()
		{
			foreach (var onWillHideEnter in OnWillExit)
			{
				await onWillHideEnter.Invoke();
			}
		}

		void IPanelLifecycleEvent.DidExit()
		{
			OnDidExit?.Invoke();
		}

		async UniTask IPanelLifecycleEvent.Cleanup()
		{
			foreach (var onCleanup in OnCleanup)
			{
				await onCleanup.Invoke();
			}
		}
	}
}
