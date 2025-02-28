using Abstractions.Shared.Events;
using Assets.Abstractions.RPG.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events
{
    public class ActorDieEventArgs : BaseEventArgs<ActorDieEventArgs>
    {
        public ICharacter m_Actor;

        public ActorDieEventArgs(ICharacter actor)
        {
            m_Actor = actor;
        }
    }
}
