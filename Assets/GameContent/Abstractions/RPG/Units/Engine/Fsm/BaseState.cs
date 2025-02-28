using Assets.Abstractions.RPG.Units;
using UnityEngine;
using UnityEngine.Events;

namespace Abstractions.RPG.Units.Engine.Fsm
{
    public abstract class BaseState : MonoBehaviour, IState
    {
        public UnityEvent<CharacterActor> OnEnterState;
        public UnityEvent<CharacterActor> OnExitState;
        public CharacterActor Actor { set; get; }
        protected virtual void OnEnable()
        {
        }
        protected virtual void OnDisable()
        {
        }

        public virtual void Enter()
        {
            OnEnterState?.Invoke(Actor);
        }
        public virtual void Execute() { }

        public virtual void Exit()
        {
            OnExitState?.Invoke(Actor);
        }

        public virtual void Reset() { }
        public virtual void InitializeStateMachine() { }
    }
}
