using Assets.Abstractions.RPG.Attributes;
using Assets.Abstractions.RPG.Units;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstractions.RPG.Units.Engine.DamageCalculatorEngine
{
    public interface IDamageDealer
    {
        void Init(IAttributeGroup stat);
        void Release(IAttributeGroup stat);
        HitResult DealDamage(ICharacter attacker, ICharacter defender);
    }
}
