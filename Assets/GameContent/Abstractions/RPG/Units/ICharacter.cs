using UnityEngine;

namespace Assets.Abstractions.RPG.Units
{
    public interface ICharacter
    {
        bool IsInitialized { get; }
        bool IsActivated { get; }
        bool IsDead { get; set; }
        bool AI { get; set; }
        Transform Transform { get; }
        Transform GraphicTrans { get; }
        TEngine GetEngine<TEngine>() where TEngine : IEngine;

        void Initialize();
        void ActiveActor();
        void Execute();
    }
}
