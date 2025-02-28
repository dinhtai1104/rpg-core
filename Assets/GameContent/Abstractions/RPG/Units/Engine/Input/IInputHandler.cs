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
    public interface IInputHandler : IEngine
    {
        bool Active { set; get; }
        bool IsHoldingAttackButton { set; get; }
        bool IsUsingJoystick { set; get; }
        Vector2 JoystickDirection { set; get; }
        float JoystickDirectionScalar { set; get; }

        void OnUpdate();
        void SubscribeControl(EControlCode controlCode, UnityAction action);
        void UnsubscribeControl(EControlCode controlCode, UnityAction action);
        void InvokeControl(EControlCode controlCode);
    }
}
