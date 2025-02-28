using Abstractions.RPG.Misc;
using Abstractions.Shared.Events;

namespace Events
{
    public class InputButtonSkillEventArgs : BaseEventArgs<InputButtonSkillEventArgs>
    {
        public EControlCode ControlCode;
    }
}
