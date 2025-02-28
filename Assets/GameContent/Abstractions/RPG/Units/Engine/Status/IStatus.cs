using Assets.Abstractions.RPG.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstractions.RPG.Units.Engine.Status
{
    public interface IStatus
    {
        ITagger Tagger { get; }
        bool Permanent { get; }
        bool Expirable { get; }
        bool Stackable { get; }
        int MaxStack { get; }
        bool Override { get; }
        bool IsExpired { get; }
        bool IsRunning { get; }
        CharacterActor Actor { get; }
        CharacterActor Source { get; }
        void Begin();
        void Cancel();
        void Stop();
        void OnUpdate(float dt);
        void SetActor(CharacterActor actor);
        void SetSource(CharacterActor source);
        IStatus SetDuration(float duration);
        IStatus SetModifierValue(float value);
    }
}
