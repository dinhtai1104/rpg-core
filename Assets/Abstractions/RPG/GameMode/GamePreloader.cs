using Assets.Abstractions.RPG.GameServices;
using Assets.Abstractions.Shared.Loader.Core;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace Assets.Abstractions.RPG.GameMode
{
    [System.Serializable]
    public class GamePreloader
    {
        private IResourceServices resourceServices;
        private Dictionary<string, object> preloadedAssets;
        public GamePreloader(IResourceServices resourceServices)
        {
            preloadedAssets = new();
            this.resourceServices = resourceServices;
        }

        public async UniTask PreLoad<T>(string path) where T : Object
        {
            if (!preloadedAssets.ContainsKey(path))
            {
                var assets = await resourceServices.GetAsync<T>(path);
                preloadedAssets.Add(path, assets);
            }
        }

        public TAsset GetAsset<TAsset>(string path) where TAsset : Object
        {
            return (TAsset)preloadedAssets[path];
        }

        public void ReleaseAll()
        {
            foreach (var asset in preloadedAssets)
            {
                resourceServices.Release(asset.Key);
            }
        }
    }
}
