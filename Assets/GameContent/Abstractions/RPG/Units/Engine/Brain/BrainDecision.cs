using Assets.Abstractions.RPG.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Abstractions.RPG.Units.Engine.Brain
{
    public abstract class BrainDecision : ScriptableObject
    {
        public abstract bool Decide(CharacterActor actor);
    }
}
