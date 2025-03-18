using Abstractions.RPG.Misc;
using Abstractions.Shared.Events;
using Assets.Abstractions.RPG.Units;
using Assets.Abstractions.Shared.Core.DI;
using Assets.Abstractions.Shared.UnRegister;
using Assets.Game.Scripts.Events;
using Cysharp.Threading.Tasks;
using Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Abstractions.RPG.Units.Engine.Inputs
{
    public class PlayerInput : BaseInputHandler
    {
        [Inject] private IEventService _eventService;
        private bool _isHolding;

        public override bool IsHoldingAttackButton => _isHolding;

        protected override void OnDeactivate()
        {
            base.OnDeactivate();
            IsUsingJoystick = false;
            JoystickDirection = Vector2.zero;
        }

        private async void OnEnable()
        {
            await UniTask.Delay(200).UnRegister(this);
            _eventService.Subscribe<JoystickMovementStartEventArgs>(OnJoystickMovementStart);
            _eventService.Subscribe<JoystickMovementEventArgs>(OnJoystickMovement);
            _eventService.Subscribe<JoystickMovementEndEventArgs>(OnJoystickMovementEnd);
            _eventService.Subscribe<InputButtonSkillEventArgs>(OnInputButtonSkill);
        }
        private void OnDisable()
        {
            _eventService.Unsubscribe<JoystickMovementStartEventArgs>(OnJoystickMovementStart);
            _eventService.Unsubscribe<JoystickMovementEventArgs>(OnJoystickMovement);
            _eventService.Unsubscribe<JoystickMovementEndEventArgs>(OnJoystickMovementEnd);
            _eventService.Unsubscribe<InputButtonSkillEventArgs>(OnInputButtonSkill);
        }


#if UNITY_EDITOR
        private void Update()
        {
            var dash = Input.GetKeyDown(KeyCode.Space);
            if (dash)
            {
                InvokeControl(EControlCode.Dash);
            }
        }
#endif

        private void OnJoystickMovementStart(object sender, IEventArgs e)
        {
            IsUsingJoystick = true;
        }

        private void OnJoystickMovement(object sender, IEventArgs e)
        {
            if (!Active) return;
            var evt = e as JoystickMovementEventArgs;
            var normalizedDir = evt.m_Direction.normalized;
            JoystickDirection = normalizedDir;
            InvokeControl(EControlCode.Move);
        }

        private void OnJoystickMovementEnd(object sender, IEventArgs e)
        {
            IsUsingJoystick = false;
            JoystickDirection = Vector2.zero;
        }

        private void OnInputButtonSkill(object sender, IEventArgs e)
        {
            if (!Active) return;
            var evt = e as InputButtonSkillEventArgs;
            InvokeControl(evt.ControlCode);
        }
    }
}
