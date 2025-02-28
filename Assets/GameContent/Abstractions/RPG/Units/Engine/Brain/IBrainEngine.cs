using Assets.Abstractions.RPG.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstractions.RPG.Units.Engine.Brain
{
    public interface IBrainEngine : IEngine
    {
        CharacterActor Owner { get; }
        void Init(CharacterActor actor);
        void OnUpdate();
    }
}
