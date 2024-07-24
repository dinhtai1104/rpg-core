namespace Assets.Abstractions.RPG.Units
{
    public interface ICharacter
    {
        bool IsInitialized { get; }
        bool IsActivated { get; }
        TEngine GetEngine<TEngine>() where TEngine : IEngine;

        void Initialize();
        void ActiveActor();
        void Execute();
    }
}
