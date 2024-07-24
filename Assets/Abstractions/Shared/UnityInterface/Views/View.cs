using System.Collections.Generic;
using Assets.Abstractions.Shared.Foundation;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Abstractions.Shared.UnityInterface
{
	[RequireComponent(typeof(RectTransform))]
	public class View : UIBehaviour, IView
	{
		[SerializeField] private bool _usePrefabNameAsIdentifier = true;
		[SerializeField, HideIf("_usePrefabNameAsIdentifier")] private string _identifier;
		[SerializeField, HideInInspector] private RectTransform _rectTransform;
		[SerializeField, HideInInspector] private CanvasGroup _canvasGroup;
		[SerializeField, HideInInspector] private RectTransform _parent;

		public string Identifier
		{
			get => _identifier;
			set => _identifier = value;
		}

		public virtual string Name
		{
			get { return !IsDestroyed() && gameObject == true ? gameObject.name : string.Empty; }

			set
			{
				if (IsDestroyed() || gameObject == false)
				{
					return;
				}

				gameObject.name = value;
			}
		}

		public virtual RectTransform RectTransform
		{
			get
			{
				if (IsDestroyed())
				{
					return null;
				}

				if (_rectTransform == false)
				{
					_rectTransform = gameObject.GetOrAddComponent<RectTransform>();
				}

				return _rectTransform;
			}
		}

		public virtual CanvasGroup CanvasGroup
		{
			get
			{
				if (IsDestroyed())
				{
					return null;
				}

				if (_canvasGroup == false)
				{
					_canvasGroup = gameObject.GetOrAddComponent<CanvasGroup>();
				}

				return _canvasGroup;
			}
		}

		public virtual RectTransform Parent
		{
			get
			{
				if (IsDestroyed())
				{
					return null;
				}

				return _parent;
			}

			internal set => _parent = value;
		}

		public virtual GameObject Owner => IsDestroyed() ? null : gameObject;

		public virtual bool ActiveSelf
		{
			get
			{
				GameObject o;
				return IsDestroyed() == false && (o = gameObject) == true && o.activeSelf;
			}

			set
			{
				if (IsDestroyed() || gameObject == false || gameObject.activeSelf == value)
				{
					return;
				}

				gameObject.SetActive(value);
			}
		}

		public virtual float Alpha
		{
			get
			{
				if (IsDestroyed() || gameObject == false)
				{
					return 0;
				}

				if (CanvasGroup)
				{
					return CanvasGroup.alpha;
				}

				return 1f;
			}
			set
			{
				if (IsDestroyed() || gameObject == false)
				{
					return;
				}

				if (CanvasGroup)
				{
					CanvasGroup.alpha = value;
				}
			}
		}

		public virtual bool Interactable
		{
			get
			{
				if (IsDestroyed() || gameObject == false)
				{
					return false;
				}

				if (CanvasGroup)
				{
					return CanvasGroup.interactable;
				}

				return true;
			}

			set
			{
				if (IsDestroyed() || gameObject == false)
				{
					return;
				}

				if (CanvasGroup)
				{
					CanvasGroup.interactable = value;
				}
			}
		}

		public virtual UISettings Settings { get; set; }

		protected void SetIdentifier()
		{
			_identifier = _usePrefabNameAsIdentifier
				? gameObject.name.Replace("(Clone)", string.Empty)
				: _identifier;
		}

		protected static async UniTask WaitForAsync(IEnumerable<UniTask> tasks)
		{
			try
			{
				foreach (var task in tasks)
				{
					try
					{
						await task;
					}
					catch (System.Exception ex)
					{
						Debug.LogException(ex);
					}
				}
			}
			catch (System.Exception ex)
			{
				Debug.LogException(ex);
			}
		}
	}
}
