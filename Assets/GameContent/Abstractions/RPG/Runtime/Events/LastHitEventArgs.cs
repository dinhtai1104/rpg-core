using Abstractions.Shared.Events;
using Assets.Abstractions.RPG.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Game.Scripts.Events
{
    public class LastHitEventArgs : BaseEventArgs<LastHitEventArgs>
    {
        public ICharacter m_Killer;
        public ICharacter m_Patient;

        public LastHitEventArgs(ICharacter killer, ICharacter patient)
        {
            m_Killer = killer;
            m_Patient = patient;
        }
    }
}
