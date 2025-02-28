using Abstractions.Shared.Events;
using Assets.Abstractions.RPG.Units;

namespace Events
{
    public class HealthFullEventEventArgs : BaseEventArgs<HealthFullEventEventArgs>
    {
        public ICharacter m_Actor;

        public HealthFullEventEventArgs(ICharacter actor)
        {
            m_Actor = actor;
        }
    }
}
