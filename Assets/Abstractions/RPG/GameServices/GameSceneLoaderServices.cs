using Assets.Abstractions.RPG.LocalData;
using Assets.Abstractions.Shared.Core;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;

namespace Assets.Abstractions.RPG.GameServices
{
    public interface IGameSceneLoaderServices : IService, IInitializable
    {
        UniTask LoadScene(SceneLoader sceneData, CancellationToken cts);
        void ActiveScene(string sceneKey);
    }

    [Service(typeof(IGameSceneLoaderServices))]
    public class GameSceneLoaderServices : IGameSceneLoaderServices
    {
        public bool Initialized { set; get; }
        public int Priority => -1;
        private Dictionary<string, SceneLoader> _sceneLoading;

        public UniTask OnInitialize(IArchitecture architecture)
        {
            _sceneLoading = new();
            Initialized = true;
            return UniTask.CompletedTask;
        }

        public async UniTask LoadScene(SceneLoader sceneData, CancellationToken cts)
        {
            _sceneLoading.TryAdd(sceneData.Key, sceneData);
            await sceneData.LoadAsync(cts);

        }

        public void ActiveScene(string sceneKey)
        {
            _sceneLoading[sceneKey].ActiveScene();
            _sceneLoading.Remove(sceneKey);
        }
    }
}
