using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Abstractions.Shared.UnityInterface
{
	[RequireComponent(typeof(CanvasGroup))]
	public class ModalBackdrop : View
	{
		[SerializeField] private ModalBackdropTransitionAnimationContainer _animationContainer;
		[SerializeField] private bool _closeModalWhenClicked;

		public ModalBackdropTransitionAnimationContainer AnimationContainer => _animationContainer;

		private Image _image;
		private float _originalAlpha;
		private ModalContainer _modalContainer;

		protected override void Awake()
		{
			_image = GetComponent<Image>();
			_originalAlpha = _image ? _image.color.a : 1f;
			SetCloseModalOnClick(_closeModalWhenClicked);
		}

		public void Setup(RectTransform parent, in float? alpha, in bool? closeModalWhenClick)
		{
			SetAlpha(alpha);
			SetCloseModalOnClick(closeModalWhenClick);

			Parent = parent;
			RectTransform.FillParent(Parent);
			CanvasGroup.interactable = _closeModalWhenClicked;

			_modalContainer = parent.GetComponent<ModalContainer>();

			gameObject.SetActive(false);
		}

		private void SetAlpha(in float? value)
		{
			var image = _image;
			if (image == false)
			{
				return;
			}

			var alpha = _originalAlpha;
			if (value.HasValue)
			{
				alpha = value.Value;
			}

			var color = image.color;
			color.a = alpha;
			image.color = color;
		}

		private void SetCloseModalOnClick(in bool? value)
		{
			if (value.HasValue)
			{
				_closeModalWhenClicked = value.Value;
			}

			if (_closeModalWhenClicked)
			{
				if (TryGetComponent<Image>(out var image) == false)
				{
					image = gameObject.AddComponent<Image>();
					image.color = Color.clear;
				}

				if (TryGetComponent<Button>(out var button) == false)
				{
					button = gameObject.AddComponent<Button>();
					button.transition = Selectable.Transition.None;
				}

				button.onClick.AddListener(CloseModalOnClick);
			}
			else
			{
				if (TryGetComponent<Button>(out var button))
				{
					button.onClick.RemoveListener(CloseModalOnClick);
					Destroy(button);
				}
			}
		}

		private void CloseModalOnClick()
		{
			if (_modalContainer == null)
			{
				return;
			}

			if (_modalContainer.IsInTransition)
			{
				return;
			}

			_modalContainer.Pop(true);
		}

		internal async UniTask EnterAsync(bool playAnimation)
		{
			gameObject.SetActive(true);
			RectTransform.FillParent(Parent);
			CanvasGroup.alpha = 1f;

			if (playAnimation)
			{
				var anim = GetAnimation(true);
				anim.Setup(RectTransform);

				await anim.PlayAsync();
			}

			RectTransform.FillParent(Parent);
		}

		internal async UniTask ExitAsync(bool playAnimation)
		{
			gameObject.SetActive(true);
			RectTransform.FillParent(Parent);
			CanvasGroup.alpha = 1f;

			if (playAnimation)
			{
				var anim = GetAnimation(false);
				anim.Setup(RectTransform);

				await anim.PlayAsync();
			}

			CanvasGroup.alpha = 0f;
			gameObject.SetActive(false);
		}

		private ITransitionAnimation GetAnimation(bool enter)
		{
			var anim = _animationContainer.GetAnimation(enter);
			if (anim == null)
			{
				return Settings.GetDefaultModalBackdropTransitionAnimation(enter);
			}

			return anim;
		}
	}
}
