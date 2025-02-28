using Assets.Abstractions.RPG.Units;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeReferences;
using UnityEngine;

namespace Abstractions.RPG.Units.Engine.Fsm
{
    public class FSMEngine : BaseMonoEngine, IFsm
    {
        [SerializeField] private Transform statesHolder;

        [SerializeField, ClassImplements(typeof(IState))]
        private ClassTypeReference _startingState;

        [ShowInInspector]
        public Dictionary<Type, IState> states = new Dictionary<Type, IState>();

        [ShowInInspector]
        private IState InitialState { set; get; }
        [ShowInInspector]
        private IState currentState { set; get; }
        [ShowInInspector]
        private IState NextState { set; get; }
        [ShowInInspector]
        private IState PreviousState { set; get; }

        public bool OnEnter { set; get; }
        public bool OnExit { set; get; }

        public override void Initialize(ICharacter character)
        {
            base.Initialize(character);
            var allState = statesHolder.GetComponentsInChildren<IState>();
            states.Clear();
            foreach (var e in allState)
            {
                states.Add(e.GetType(), e);
                e.Actor = Owner;
            }
            AddState(EmptyState.NullState);

            // If initial state was not set
            if (InitialState == null)
            {
                // If starting type was set
                if (_startingState.Type != default)
                    SetInitialState(_startingState.Type);
                // If not then start at Empty State
                else
                    InitialState = EmptyState.NullState;
            }

            PreviousState = InitialState;
            currentState = InitialState;
            if (null == currentState)
            {
                throw new Exception("\n" + name + ".nextState is null on Initialize()!\tDid you forget to call SetInitialState()?\n");
            }

            foreach (KeyValuePair<Type, IState> pair in states)
            {
                pair.Value.InitializeStateMachine();
            }

            OnEnter = true;
            OnExit = false;
        }

        public void BackToDefaultState()
        {
            ChangeState(_startingState);
        }

        public void ChangeToEmptyState()
        {
            NextState = EmptyState.NullState;
            OnExit = true;
        }

        public void OnUpdate()
        {
            if (OnExit)
            {
                if (currentState != null)
                {
                    currentState.Exit();
                }
                PreviousState = currentState;
                currentState = NextState;
                NextState = null;

                OnEnter = true;
                OnExit = false;
            }

            if (OnEnter)
            {
                currentState.Enter();

                OnEnter = false;
            }

            try
            {
                currentState.Execute();
            }
            catch (Exception e)
            {

            }
        }

        public void SetInitialState<T>() where T : IState
        {
            InitialState = states[typeof(T)];
        }

        public void SetInitialState(Type T)
        {
            InitialState = states[T];
        }

        public virtual void ChangeState<T>() where T : IState
        {
            ChangeState(typeof(T));
        }

        public virtual void ChangeState(Type T)
        {

            if (!states.ContainsKey(T))
            {
                Debug.LogError("Missing state " + T.Name + " on machine " + gameObject.name);
                return;
            }

            NextState = states[T];

            OnExit = true;
        }

        public bool IsPreviousState<T>(bool allowBaseType = false) where T : IState
        {
            return allowBaseType ? PreviousState.GetType().BaseType == typeof(T) : PreviousState.GetType() == typeof(T);
        }

        public bool IsCurrentState<T>(bool allowBaseType = false) where T : IState
        {
            return allowBaseType ? currentState is T : currentState.GetType() == typeof(T);
        }

        public bool IsCurrentState(Type T)
        {
            return currentState.GetType() == T;
        }

        public bool AddState<T>(IState state)
        {
            var type = typeof(T);

            if (HasState(type)) return false;
            state.Actor = Owner;
            states.Add(type, state);
            return true;
        }

        public bool AddState(IState state)
        {
            var type = state.GetType();

            if (HasState(type)) return false;
            state.Actor = Owner;
            states.Add(type, state);
            return true;
        }

        public bool AddState(Type T)
        {
            if (HasState(T)) return false;
            var item = GetComponent(T);

            if (item == null)
            {
                item = gameObject.AddComponent(T);
            }

            ((IState)item).Actor = Owner;

            states.Add(T, (IState)item);
            return true;
        }

        public void RemoveState<T>() where T : IState
        {
            states.Remove(typeof(T));
        }

        public void RemoveState(Type T)
        {
            states.Remove(T);
        }

        public bool HasState<T>() where T : IState
        {
            return states.ContainsKey(typeof(T));
        }

        public bool HasState(Type T)
        {
            return states.ContainsKey(T);
        }

        public void RemoveAllStates()
        {
            states.Clear();
        }

        public T CurrentState<T>() where T : IState
        {
            return (T)currentState;
        }

        public IState CurrentState()
        {
            return currentState;
        }

        public T GetState<T>() where T : IState
        {
            if (states.ContainsKey(typeof(T)))
                return (T)states[typeof(T)];
            else
                Debug.LogError("Missing State: " + typeof(T));
            return default;
        }

        public IState GetState(Type type)
        {
            if (states.ContainsKey(type))
                return states[type];
            else
                Debug.LogError("Missing State: " + type);
            return null;
        }

        public void Reset()
        {
            PreviousState = InitialState;
            currentState = InitialState;
            OnEnter = true;
            OnExit = false;

            foreach (KeyValuePair<Type, IState> pair in states)
            {
                pair.Value.Reset();
            }
        }

        [Button]
        public void ChangeToIdleState()
        {
        }
    }
}
