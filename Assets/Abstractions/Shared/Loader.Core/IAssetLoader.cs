using UnityEngine;

namespace Assets.Abstractions.Shared.Loader.Core
{
	public interface IAssetLoader
	{
		AssetRequest<TAsset> Load<TAsset>(string address) where TAsset : Object;

		AssetRequest<TAsset> LoadAsync<TAsset>(string address) where TAsset : Object;

		void Release(AssetRequest request);
	}
}
