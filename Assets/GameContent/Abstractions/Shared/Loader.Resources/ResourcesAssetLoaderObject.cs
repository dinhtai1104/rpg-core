using Assets.Abstractions.Shared.Loader.Core;
using UnityEngine;

namespace Assets.Abstractions.Shared.Loader.Resource
{
	[CreateAssetMenu(fileName = "ResourcesAssetLoader", menuName = "Sparkle/Asset Loader/Resources Asset Loader")]
	public class ResourcesAssetLoaderObject : AssetLoaderObject, IAssetLoader
	{
		private readonly ResourcesAssetLoader _loader = new ResourcesAssetLoader();

		public override AssetRequest<TAsset> Load<TAsset>(string address)
		{
			return _loader.Load<TAsset>(address);
		}

		public override AssetRequest<TAsset> LoadAsync<TAsset>(string address)
		{
			return _loader.Load<TAsset>(address);
		}

		public override void Release(AssetRequest request)
		{
			_loader.Release(request);
		}
	}
}
