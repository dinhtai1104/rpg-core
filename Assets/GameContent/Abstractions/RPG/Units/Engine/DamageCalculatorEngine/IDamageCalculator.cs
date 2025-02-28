using Assets.Abstractions.RPG.Attributes;
using Assets.Abstractions.RPG.Misc;
using Assets.Abstractions.RPG.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstractions.RPG.Units.Engine.DamageCalculatorEngine
{
    public interface IDamageCalculator : IEngine
    {
        CharacterActor Owner { get; }
        void Init(CharacterActor actor);
        void SetImmuneDamageType(EDamageType type, bool immune);
        void AddAttackerTemporaryModifier(string modName, AttributeModifier mod);
        void AddDefenderTemporaryModifier(string modName, AttributeModifier mod);
        HitResult CalculateDamage(CharacterActor defender, CharacterActor attacker, DamageSource source);
    }
}
