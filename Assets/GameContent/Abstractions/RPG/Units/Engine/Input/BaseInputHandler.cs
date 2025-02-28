using Abstractions.RPG.Misc;
using Assets.Abstractions.RPG.Units;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Abstractions.RPG.Units.Engine.Inputs
{
    public class BaseInputHandler : BaseMonoEngine, IInputHandler
    {
        private Dictionary<EControlCode, UnityEvent> _listenerLookup;
        private IInputHandler _controlStrategy;
        private bool _active;

        [ShowInInspector]
        public virtual bool Active
        {
            set
            {
                if (_active == value) return;
                _active = value;

                if (_active)
                {
                    OnActivate();
                }
                else
                {
                    OnDeactivate();
                }
            }
            get => _active;
        }

        [ShowInInspector] public virtual bool Lock { set; get; }

        [ShowInInspector] public virtual bool IsHoldingAttackButton { set; get; }
        public virtual bool IsUsingJoystick { set; get; }
        [ShowInInspector] public virtual Vector2 JoystickDirection { set; get; }
        [ShowInInspector] public virtual float JoystickDirectionScalar { set; get; }

        public override void Initialize(ICharacter character)
        {
            base.Initialize(character);
            _listenerLookup = new Dictionary<EControlCode, UnityEvent>();
        }

        public virtual void InvokeControl(EControlCode controlCode)
        {
            if (_listenerLookup.ContainsKey(controlCode)) _listenerLookup[controlCode].Invoke();
        }

        public virtual void SubscribeControl(EControlCode controlCode, UnityAction action)
        {
            if (_listenerLookup.ContainsKey(controlCode))
            {
                _listenerLookup[controlCode].AddListener(action);
            }
            else
            {
                var listener = new UnityEvent();
                listener.AddListener(action);
                _listenerLookup.Add(controlCode, listener);
            }
        }

        public virtual void UnsubscribeControl(EControlCode controlCode, UnityAction action)
        {
            if (_listenerLookup.ContainsKey(controlCode))
            {
                _listenerLookup[controlCode].RemoveListener(action);
            }
        }

        public virtual void OnUpdate()
        {
        }

        protected virtual void OnActivate()
        {
        }

        protected virtual void OnDeactivate()
        {
        }

        public virtual void StopMovement()
        {
            IsUsingJoystick = false;
            JoystickDirection = Vector2.zero;
        }
    }
}
