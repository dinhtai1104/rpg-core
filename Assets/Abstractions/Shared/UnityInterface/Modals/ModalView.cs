using System;
using Assets.Abstractions.Shared.Foundation;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Abstractions.Shared.UnityInterface
{
	[DisallowMultipleComponent]
	public class ModalView : View, IModalLifecycleEvent
	{
		[SerializeField] private ModalTransitionAnimationContainer _animationContainer = new();

		private Progress<float> _transitionProgressReporter;
		private readonly UniquePriorityList<IModalLifecycleEvent> _lifecycleEvents = new();

		private Progress<float> TransitionProgressReporter
		{
			get { return _transitionProgressReporter ??= new Progress<float>(SetTransitionProgress); }
		}

		public ModalTransitionAnimationContainer AnimationContainer => _animationContainer;

		public bool IsTransitioning { get; private set; }

		public ModalTransitionAnimationType? TransitionAnimationType { get; private set; }

		public float TransitionAnimationProgress { get; private set; }

		public event Action<float> TransitionAnimationProgressChanged;

		public virtual UniTask Initialize()
		{
			return UniTask.CompletedTask;
		}

		public virtual UniTask WillPushEnter()
		{
			return UniTask.CompletedTask;
		}

		public virtual void DidPushEnter() { }

		public virtual UniTask WillPushExit()
		{
			return UniTask.CompletedTask;
		}

		public virtual void DidPushExit() { }

		public virtual UniTask WillPopEnter()
		{
			return UniTask.CompletedTask;
		}

		public virtual void DidPopEnter() { }

		public virtual UniTask WillPopExit()
		{
			return UniTask.CompletedTask;
		}

		public virtual void DidPopExit() { }

		public virtual UniTask Cleanup()
		{
			return UniTask.CompletedTask;
		}

		public void AddLifecycleEvent(IModalLifecycleEvent lifecycleEvent, int priority = 0)
		{
			_lifecycleEvents.Add(lifecycleEvent, priority);
		}

		public void RemoveLifecycleEvent(IModalLifecycleEvent lifecycleEvent)
		{
			_lifecycleEvents.Remove(lifecycleEvent);
		}

		internal async UniTask AfterLoadAsync(RectTransform parentTransform)
		{
			_lifecycleEvents.Add(this, 0);
			SetIdentifier();

			Parent = parentTransform;
			RectTransform.FillParent(Parent);

			Alpha = 0.0f;

			var tasks = _lifecycleEvents.Select(x => x.Initialize());
			await WaitForAsync(tasks);
		}

		internal async UniTask BeforeEnterAsync(bool push)
		{
			IsTransitioning = true;

			if (push)
			{
				TransitionAnimationType = ModalTransitionAnimationType.Enter;
				gameObject.SetActive(true);
				RectTransform.FillParent(Parent);
				Alpha = 0.0f;
			}

			SetTransitionProgress(0.0f);

			var tasks = push
				? _lifecycleEvents.Select(x => x.WillPushEnter())
				: _lifecycleEvents.Select(x => x.WillPopEnter());

			await WaitForAsync(tasks);
		}

		internal async UniTask EnterAsync(bool push, bool playAnimation, ModalView partnerModal)
		{
			if (push)
			{
				Alpha = 1.0f;

				if (playAnimation)
				{
					var anim = GetAnimation(true, partnerModal);

					if (partnerModal == true)
					{
						anim.SetPartner(partnerModal.RectTransform);
					}

					anim.Setup(RectTransform);
					await anim.PlayAsync(TransitionProgressReporter);
				}

				RectTransform.FillParent(Parent);
			}

			SetTransitionProgress(1.0f);
		}

		internal void AfterEnter(bool push)
		{
			if (push)
			{
				foreach (var lifecycleEvent in _lifecycleEvents)
				{
					lifecycleEvent.DidPushEnter();
				}
			}
			else
			{
				foreach (var lifecycleEvent in _lifecycleEvents)
				{
					lifecycleEvent.DidPopEnter();
				}
			}

			IsTransitioning = false;
			TransitionAnimationType = null;
		}

		internal async UniTask BeforeExitAsync(bool push)
		{
			IsTransitioning = true;

			if (push == false)
			{
				TransitionAnimationType = ModalTransitionAnimationType.Exit;
				gameObject.SetActive(true);
				RectTransform.FillParent(Parent);
				Alpha = 1.0f;
			}

			SetTransitionProgress(0.0f);

			var tasks = push
				? _lifecycleEvents.Select(x => x.WillPushExit())
				: _lifecycleEvents.Select(x => x.WillPopExit());

			await WaitForAsync(tasks);
		}

		internal async UniTask ExitAsync(bool push, bool playAnimation, ModalView partnerModal)
		{
			if (push == false)
			{
				if (playAnimation)
				{
					var anim = GetAnimation(false, partnerModal);

					if (partnerModal == true)
					{
						anim.SetPartner(partnerModal.RectTransform);
					}

					anim.Setup(RectTransform);
					await anim.PlayAsync(TransitionProgressReporter);
				}

				Alpha = 0.0f;
			}

			SetTransitionProgress(1.0f);
		}

		internal void AfterExit(bool push)
		{
			if (push)
			{
				foreach (var lifecycleEvent in _lifecycleEvents)
				{
					lifecycleEvent.DidPushExit();
				}
			}
			else
			{
				foreach (var lifecycleEvent in _lifecycleEvents)
				{
					lifecycleEvent.DidPopExit();
				}
			}

			IsTransitioning = false;
			TransitionAnimationType = null;
		}

		internal async UniTask BeforeReleaseAsync()
		{
			var tasks = _lifecycleEvents.Select(x => x.Cleanup());
			await WaitForAsync(tasks);
		}

		private void SetTransitionProgress(float progress)
		{
			TransitionAnimationProgress = progress;
			TransitionAnimationProgressChanged?.Invoke(progress);
		}

		private ITransitionAnimation GetAnimation(bool enter, ModalView partner)
		{
			var partnerIdentifier = partner == true ? partner.Identifier : string.Empty;
			var anim = _animationContainer.GetAnimation(enter, partnerIdentifier);

			if (anim == null)
			{
				return Settings.GetDefaultModalTransitionAnimation(enter);
			}

			return anim;
		}
	}
}
