using System;
using Assets.Abstractions.Shared.Foundation;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Abstractions.Shared.UnityInterface
{
	[DisallowMultipleComponent]
	public class PanelView : View, IPanelLifecycleEvent
	{
		[SerializeField] private PanelTransitionAnimationContainer _animationContainer = new();

		private Progress<float> _transitionProgressReporter;
		private readonly UniquePriorityList<IPanelLifecycleEvent> _lifecycleEvents = new();

		private Progress<float> TransitionProgressReporter
		{
			get { return _transitionProgressReporter ??= new Progress<float>(SetTransitionProgress); }
		}

		public PanelTransitionAnimationContainer AnimationContainer => _animationContainer;

		public bool IsTransitioning { get; private set; }

		public PanelTransitionAnimationType? TransitionAnimationType { get; private set; }

		public float TransitionAnimationProgress { get; private set; }

		public event Action<float> TransitionAnimationProgressChanged;

		public virtual UniTask Initialize()
		{
			return UniTask.CompletedTask;
		}

		public virtual UniTask WillEnter()
		{
			return UniTask.CompletedTask;
		}

		public virtual void DidEnter() { }

		public virtual UniTask WillExit()
		{
			return UniTask.CompletedTask;
		}

		public virtual void DidExit() { }

		public virtual UniTask Cleanup()
		{
			return UniTask.CompletedTask;
		}

		public void AddLifecycleEvent(IPanelLifecycleEvent lifecycleEvent, int priority = 0)
		{
			_lifecycleEvents.Add(lifecycleEvent, priority);
		}

		public void RemoveLifecycleEvent(IPanelLifecycleEvent lifecycleEvent)
		{
			_lifecycleEvents.Remove(lifecycleEvent);
		}

		public void SetSortingLayer(SortingLayerId? layer, int? sortingOrder)
		{
			if ((layer.HasValue & sortingOrder.HasValue) == false)
			{
				return;
			}

			var canvas = this.GetOrAddComponent<Canvas>();
			this.GetOrAddComponent<GraphicRaycaster>();

			canvas.overrideSorting = true;
			canvas.sortingLayerID = layer.Value.ID;
			canvas.sortingOrder = sortingOrder.Value;
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

		internal async UniTask BeforeEnterAsync(bool show)
		{
			IsTransitioning = true;
			TransitionAnimationType = show
				? PanelTransitionAnimationType.Enter
				: PanelTransitionAnimationType.Exit;

			gameObject.SetActive(true);
			RectTransform.FillParent(Parent);
			SetTransitionProgress(0.0f);

			Alpha = 0.0f;

			var tasks = show
				? _lifecycleEvents.Select(x => x.WillEnter())
				: _lifecycleEvents.Select(x => x.WillExit());

			await WaitForAsync(tasks);
		}

		internal async UniTask EnterAsync(bool show, bool playAnimation)
		{
			Alpha = 1.0f;

			if (playAnimation)
			{
				var anim = GetAnimation(show);
				anim.Setup(RectTransform);

				await anim.PlayAsync(TransitionProgressReporter);
			}

			RectTransform.FillParent(Parent);
			SetTransitionProgress(1.0f);
		}

		internal void AfterEnter(bool show)
		{
			if (show)
			{
				foreach (var lifecycleEvent in _lifecycleEvents)
				{
					lifecycleEvent.DidEnter();
				}
			}
			else
			{
				foreach (var lifecycleEvent in _lifecycleEvents)
				{
					lifecycleEvent.DidExit();
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

		private ITransitionAnimation GetAnimation(bool enter)
		{
			var anim = _animationContainer.GetAnimation(enter);

			if (anim == null)
			{
				return Settings.GetDefaultPanelTransitionAnimation(enter);
			}

			return anim;
		}
	}
}
