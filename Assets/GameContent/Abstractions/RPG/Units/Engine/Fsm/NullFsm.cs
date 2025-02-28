using Assets.Abstractions.RPG.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstractions.RPG.Units.Engine.Fsm
{
    public class NullFsm : BaseNullEngine, IFsm
    {
        public CharacterActor Actor { set; get; }

        public bool AddState(IState state)
        {
            return false;
        }

        public bool AddState<T>(IState state)
        {
            return false;
        }

        public bool AddState(Type T)
        {
            return false;
        }

        public void BackToDefaultState()
        {
        }

        public void ChangeState<T>() where T : IState
        {
        }

        public void ChangeState(Type T)
        {
        }

        public void ChangeToEmptyState()
        {
        }

        public T GetState<T>() where T : IState
        {
            return default;
        }

        public bool HasState<T>() where T : IState
        {
            return false;
        }

        public bool HasState(Type T)
        {
            return false;
        }

        public void Init(CharacterActor actor)
        {
        }

        public bool IsCurrentState<T>(bool allowBaseType = false) where T : IState
        {
            return false;
        }

        public bool IsCurrentState(Type T)
        {
            return false;
        }

        public void RemoveAllStates()
        {
        }

        public void RemoveState<T>() where T : IState
        {
        }

        public void RemoveState(Type T)
        {
        }

        public void Reset()
        {
        }

        public void SetInitialState<T>() where T : IState
        {
        }

        public void SetInitialState(Type T)
        {
        }

        public void OnUpdate()
        {
        }
    }
}
