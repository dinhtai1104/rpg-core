using Assets.Abstractions.RPG.LocalData.Gameplay;
using Assets.Abstractions.RPG.Misc;
using Assets.Abstractions.Shared.Core;
using Cysharp.Threading.Tasks;

namespace Assets.Abstractions.RPG.GameMode
{
    public interface IGameMode
    {
        EGameMode Mode { get; }
        bool IsEndGame { get; }
        void SetData(IUserGameplayData userGameplayData);
        UniTask PreloadGame();
        void Enter();
        void StartMode();
        void OnExecute();
        void End();
    }
}
