namespace Assets.Abstractions.RPG.GameMode
{
    public interface IGameMode
    {
        bool IsEndGame { get; }
        void Enter();
        void StartMode();
        void OnExecute();
        void End();
    }
}
