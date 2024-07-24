using Assets.Abstractions.Shared.Core;
using Assets.Abstractions.Shared.Loader.Core;
using Assets.Abstractions.Shared.Loader.Resource;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Abstractions.RPG.Manager
{
    public interface IResourceManager : IService, IInitializable
    {
        TItem Get<TItem>(string path) where TItem : Object;
        UniTask<TItem> GetAsync<TItem>(string path) where TItem : Object;
    }

    [Service(typeof(IResourceManager))]
    public class ResourceManager : MonoBehaviour, IResourceManager
    {
        public bool Initialized { get; set; }
        public int Priority => 0;

        private IAssetLoader _assetLoader;

        // Cache
        private Dictionary<string, object> _cachedObjects = new();
        private Dictionary<string, AssetRequest> _cachedRequests = new();

        public TItem Get<TItem>(string path) where TItem : Object
        {
            Debug.Log($"Loading: {typeof(TItem)} - {path}");
            if (_cachedObjects.ContainsKey(path))
            {
                return (TItem)_cachedObjects[path];
            }

            var load = _assetLoader.Load<TItem>(path);
            if (load.Result != null)
            {
                _cachedRequests.Add(path, load);
                _cachedObjects.Add(path, load.Result);
                return (TItem)_cachedObjects[path];
            }
            return default(TItem);
        }
        public async UniTask<TItem> GetAsync<TItem>(string path) where TItem : Object
        {
            Debug.Log($"Loading Async: {typeof(TItem)} - {path}");
            if (_cachedObjects.ContainsKey(path))
            {
                return (TItem)_cachedObjects[path];
            }

            var load = _assetLoader.LoadAsync<TItem>(path);
            await load.Task;
            if (load.Result != null)
            {
                _cachedRequests.Add(path, load);
                _cachedObjects.Add(path, load.Result);
                return (TItem)_cachedObjects[path];
            }
            return default(TItem);
        }
        public UniTask OnInitialize(IArchitecture architecture)
        {
            Initialized = true;
            _assetLoader = new ResourcesAssetLoader();
            return UniTask.CompletedTask;
        }
    }
}
