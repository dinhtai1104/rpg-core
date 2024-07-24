using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Sirenix.OdinInspector;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Abstractions.Shared.UnityInterface
{
	[Serializable]
	public class ModalTransitionAnimationContainer
	{
		[SerializeField] private List<TransitionAnimation> _enterAnimations = new();
		[SerializeField] private List<TransitionAnimation> _exitAnimations = new();

		public List<TransitionAnimation> EnterAnimations => _enterAnimations;
		public List<TransitionAnimation> ExitAnimations => _exitAnimations;

		public ITransitionAnimation GetAnimation(bool enter, string partnerTransitionIdentifier)
		{
			var anims = enter ? _enterAnimations : _exitAnimations;
			var anim = anims.FirstOrDefault(x => x.IsValid(partnerTransitionIdentifier));
			var result = anim?.GetAnimation();
			return result;
		}

		[Serializable]
		public class TransitionAnimation
		{
			[SerializeField] private string _partnerModalIdentifierRegex;

			[SerializeField] private AnimationAssetType _assetType;

			[SerializeField, ShowIf("_assetType", AnimationAssetType.MonoBehaviour)]
			private TransitionAnimationBehaviour _animationBehaviour;

			[SerializeField, ShowIf("_assetType", AnimationAssetType.ScriptableObject)]
			private TransitionAnimationObject _animationObject;

			private Regex _partnerSheetIdentifierRegexCache;

			public string PartnerModalIdentifierRegex
			{
				get => _partnerModalIdentifierRegex;
				set => _partnerModalIdentifierRegex = value;
			}

			public AnimationAssetType AssetType
			{
				get => _assetType;
				set => _assetType = value;
			}

			public TransitionAnimationBehaviour AnimationBehaviour
			{
				get => _animationBehaviour;
				set => _animationBehaviour = value;
			}

			public TransitionAnimationObject AnimationObject
			{
				get => _animationObject;
				set => _animationObject = value;
			}

			public bool IsValid(string partnerModalIdentifier)
			{
				if (GetAnimation() == null)
				{
					return false;
				}

				if (string.IsNullOrEmpty(_partnerModalIdentifierRegex))
				{
					return true;
				}

				if (string.IsNullOrEmpty(partnerModalIdentifier))
				{
					return false;
				}

				if (_partnerSheetIdentifierRegexCache == null)
				{
					_partnerSheetIdentifierRegexCache = new Regex(_partnerModalIdentifierRegex);
				}

				return _partnerSheetIdentifierRegexCache.IsMatch(partnerModalIdentifier);
			}

			public ITransitionAnimation GetAnimation()
			{
				switch (_assetType)
				{
					case AnimationAssetType.MonoBehaviour:
						return _animationBehaviour;
					case AnimationAssetType.ScriptableObject:
						return Object.Instantiate(_animationObject);
					default:
						throw new ArgumentOutOfRangeException();
				}
			}
		}
	}
}
