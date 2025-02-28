using Abstractions.Shared.Events;
using Assets.Abstractions.RPG.Units;

namespace Events
{
    public class HealthZeroEventArgs : BaseEventArgs<HealthZeroEventArgs>
    {
        public ICharacter m_Actor;

        public HealthZeroEventArgs(ICharacter actor)
        {
            m_Actor = actor;
        }
    }
}
