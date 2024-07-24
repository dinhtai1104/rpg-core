using System;
using Assets.Abstractions.Shared.Foundation;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Abstractions.Shared.UnityInterface
{
	[DisallowMultipleComponent]
	public class ScreenView : View, IScreenLifecycleEvent
	{
		[SerializeField] private int _renderingOrder;
		[SerializeField] private ScreenTransitionAnimationContainer _animationContainer = new();

		public ScreenTransitionAnimationContainer AnimationContainer => _animationContainer;

		public bool IsTransitioning { get; private set; }

		public ScreenTransitionAnimationType? TransitionAnimationType { get; private set; }

		public float TransitionAnimationProgress { get; private set; }

		public event Action<float> TransitionAnimationProgressChanged;

		private readonly UniquePriorityList<IScreenLifecycleEvent> _lifecycleEvents = new();
		private Progress<float> _transitionProgressReporter;

		private Progress<float> TransitionProgressReporter
		{
			get { return _transitionProgressReporter ??= new Progress<float>(SetTransitionProgress); }
		}

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

		public void AddLifecycleEvent(IScreenLifecycleEvent lifecycleEvent, int priority = 0)
		{
			_lifecycleEvents.Add(lifecycleEvent, priority);
		}

		public void RemoveLifecycleEvent(IScreenLifecycleEvent lifecycleEvent)
		{
			_lifecycleEvents.Remove(lifecycleEvent);
		}

		internal async UniTask AfterLoadAsync(RectTransform parentTransform)
		{
			_lifecycleEvents.Add(this, 0);
			SetIdentifier();

			Parent = parentTransform;
			RectTransform.FillParent(Parent);

			var siblingIndex = 0;
			for (var i = 0; i < Parent.childCount; i++)
			{
				var child = Parent.GetChild(i);
				var childScreen = child.GetComponent<ScreenView>();

				siblingIndex = i;

				if (_renderingOrder >= childScreen._renderingOrder)
				{
					continue;
				}

				break;
			}

			RectTransform.SetSiblingIndex(siblingIndex);
			Alpha = 0.0f;

			var tasks = _lifecycleEvents.Select(x => x.Initialize());
			await WaitForAsync(tasks);
		}

		internal async UniTask BeforeEnterAsync(bool push)
		{
			IsTransitioning = true;
			TransitionAnimationType = push ? ScreenTransitionAnimationType.PushEnter : ScreenTransitionAnimationType.PopEnter;
			gameObject.SetActive(true);
			RectTransform.FillParent(Parent);
			SetTransitionProgress(0.0f);

			Alpha = 0.0f;

			var tasks = push
				? _lifecycleEvents.Select(x => x.WillPushEnter())
				: _lifecycleEvents.Select(x => x.WillPopEnter());

			await WaitForAsync(tasks);
		}

		internal async UniTask EnterAsync(bool push, bool playAnimation, ScreenView partnerScreen)
		{
			Alpha = 1.0f;

			if (playAnimation)
			{
				var anim = GetAnimation(push, true, partnerScreen);

				if (partnerScreen)
				{
					anim.SetPartner(partnerScreen.RectTransform);
				}

				anim.Setup(RectTransform);

				await anim.PlayAsync(TransitionProgressReporter);
			}

			RectTransform.FillParent(Parent);
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
			TransitionAnimationType = push
				? ScreenTransitionAnimationType.PushExit
				: ScreenTransitionAnimationType.PopExit;

			gameObject.SetActive(true);
			RectTransform.FillParent(Parent);
			SetTransitionProgress(0.0f);

			Alpha = 1.0f;

			var tasks = push
				? _lifecycleEvents.Select(x => x.WillPushExit())
				: _lifecycleEvents.Select(x => x.WillPopExit());

			await WaitForAsync(tasks);
		}

		internal async UniTask ExitAsync(bool push, bool playAnimation, ScreenView partnerScreen)
		{
			if (playAnimation)
			{
				var anim = GetAnimation(push, false, partnerScreen);

				if (partnerScreen)
				{
					anim.SetPartner(partnerScreen.RectTransform);
				}

				anim.Setup(RectTransform);

				await anim.PlayAsync(TransitionProgressReporter);
			}

			Alpha = 0.0f;
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

			gameObject.SetActive(false);
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

		private ITransitionAnimation GetAnimation(bool push, bool enter, ScreenView partner)
		{
			var partnerIdentifier = partner == true ? partner.Identifier : string.Empty;
			var anim = _animationContainer.GetAnimation(push, enter, partnerIdentifier);

			if (anim == null)
			{
				return Settings.GetDefaultScreenTransitionAnimation(push, enter);
			}

			return anim;
		}
	}
}
