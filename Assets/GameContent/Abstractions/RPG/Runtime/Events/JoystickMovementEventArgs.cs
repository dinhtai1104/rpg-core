using Abstractions.Shared.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Game.Scripts.Events
{
    public class JoystickMovementEventArgs : BaseEventArgs<JoystickMovementEventArgs>
    {
        public Vector2 m_Direction;

        public JoystickMovementEventArgs()
        {
        }
    }
}
