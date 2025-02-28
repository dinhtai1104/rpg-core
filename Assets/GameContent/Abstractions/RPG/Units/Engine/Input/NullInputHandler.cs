using Abstractions.RPG.Misc;
using Assets.Abstractions.RPG.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Abstractions.RPG.Units.Engine.Inputs
{
    public class NullInputHandler : BaseNullEngine, IInputHandler
    {
        public NullInputHandler()
        {
        }

        public bool Active
        {
            set { }
            get { return false; }
        }

        public bool Lock
        {
            set { }
            get { return true; }
        }

        public bool IsHoldingAttackButton
        {
            set { }
            get { return false; }
        }

        public bool IsUsingJoystick
        {
            set { }
            get { return false; }
        }

        public Vector2 JoystickDirection
        {
            set { }
            get { return Vector2.zero; }
        }

        public float JoystickDirectionScalar
        {
            set { }
            get { return 0f; }
        }


        public void InvokeControl(EControlCode controlCode)
        {
        }

        public void SubscribeControl(EControlCode controlCode, UnityAction action)
        {
        }

        public void OnUpdate()
        {
        }

        public void UnsubscribeControl(EControlCode controlCode, UnityAction action)
        {
        }
    }
}
