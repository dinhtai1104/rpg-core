using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Abstractions.Shared.Loader.Core
{
	[CreateAssetMenu(fileName = "PreloadedAssetLoader", menuName = "Sparkle/Asset Loader/Preloaded Asset Loader")]
	public class PreloadedAssetLoaderObject : AssetLoaderObject, IAssetLoader
	{
		[Serializable]
		public sealed class KeyAssetPair
		{
			public enum KeySourceType
			{
				InputField,
				AssetName
			}

			[SerializeField] private KeySourceType _keySource;
			[SerializeField] private string _key;
			[SerializeField] private Object _asset;

			public KeySourceType KeySource
			{
				get => _keySource;
				set => _keySource = value;
			}

			public string Key
			{
				get => GetKey();
				set => _key = value;
			}

			public Object Asset
			{
				get => _asset;
				set => _asset = value;
			}

			private string GetKey()
			{
				if (_keySource == KeySourceType.AssetName)
				{
					return _asset == null ? "" : _asset.name;
				}

				return _key;
			}
		}

		[SerializeField] private List<KeyAssetPair> _preloadedAssets = new List<KeyAssetPair>();

		public List<KeyAssetPair> PreloadedAssets => _preloadedAssets;

		private readonly PreloadedAssetLoader _loader = new PreloadedAssetLoader();

		private void OnEnable()
		{
			foreach (var preloadedAsset in _preloadedAssets)
			{
				if (string.IsNullOrEmpty(preloadedAsset.Key))
				{
					continue;
				}

				if (_loader.PreloadedAssets.ContainsKey(preloadedAsset.Key))
				{
					continue;
				}

				_loader.PreloadedAssets.Add(preloadedAsset.Key, preloadedAsset.Asset);
			}
		}

		private void OnDisable()
		{
			_loader.PreloadedAssets.Clear();
		}

		public override AssetRequest<TAsset> Load<TAsset>(string address)
		{
			return _loader.Load<TAsset>(address);
		}

		public override AssetRequest<TAsset> LoadAsync<TAsset>(string address)
		{
			return _loader.LoadAsync<TAsset>(address);
		}

		public override void Release(AssetRequest request)
		{
			_loader.Release(request);
		}
	}
}
