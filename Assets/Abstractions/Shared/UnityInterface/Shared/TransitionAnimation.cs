using System;
using System.Text.RegularExpressions;
using Sirenix.OdinInspector;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Abstractions.Shared.UnityInterface
{
	[Serializable]
	public class TransitionAnimation
	{
		[SerializeField] private string _partnerScreenIdentifierRegex;

		[SerializeField] private AnimationAssetType _assetType;

		[SerializeField, ShowIf("_assetType", AnimationAssetType.MonoBehaviour)]
		private TransitionAnimationBehaviour _animationBehaviour;

		[SerializeField, ShowIf("_assetType", AnimationAssetType.ScriptableObject)]
		private TransitionAnimationObject _animationObject;

		private Regex _partnerSheetIdentifierRegexCache;

		public string PartnerScreenIdentifierRegex
		{
			get => _partnerScreenIdentifierRegex;
			set => _partnerScreenIdentifierRegex = value;
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

		public bool IsValid(string partnerScreenIdentifier)
		{
			if (GetAnimation() == null)
			{
				return false;
			}

			if (string.IsNullOrEmpty(_partnerScreenIdentifierRegex))
			{
				return true;
			}

			if (string.IsNullOrEmpty(partnerScreenIdentifier))
			{
				return false;
			}

			if (_partnerSheetIdentifierRegexCache == null)
			{
				_partnerSheetIdentifierRegexCache = new Regex(_partnerScreenIdentifierRegex);
			}

			return _partnerSheetIdentifierRegexCache.IsMatch(partnerScreenIdentifier);
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
