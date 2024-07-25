using Assets.Abstractions.RPG.LocalData;
using Assets.Abstractions.Shared.Core;
using Cysharp.Threading.Tasks;

namespace Assets.Abstractions.RPG.GameServices
{
    public interface IGameSceneLoaderServices : IService, IInitializable
    {
        UniTask LoadScene(SceneData sceneData);
    }

    [Service(typeof(IGameSceneLoaderServices))]
    public class GameSceneLoaderServices : IGameSceneLoaderServices
    {
        public bool Initialized { set; get; }

        public int Priority => -1;

        public UniTask OnInitialize(IArchitecture architecture)
        {
            return UniTask.CompletedTask;
        }

        public UniTask LoadScene(SceneData sceneData)
        {
            return UniTask.CompletedTask;
        }
    }
}
