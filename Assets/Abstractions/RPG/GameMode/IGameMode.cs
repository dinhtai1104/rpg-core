using Assets.Abstractions.RPG.LocalData.Gameplay;
using Assets.Abstractions.RPG.Misc;
using Cysharp.Threading.Tasks;

namespace Assets.Abstractions.RPG.GameMode
{
    public interface IGameMode
    {
        EGameMode Mode { get; set; }
        bool IsEndGame { get; }
        void SetData(IUserGameplayData userGameplayData);
        UniTask PreloadGame();
        void Enter();
        void StartMode();
        void OnExecute();
        void End();
    }
}
