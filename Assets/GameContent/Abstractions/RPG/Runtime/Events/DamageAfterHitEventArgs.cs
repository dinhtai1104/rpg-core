using Abstractions.RPG.Units.Engine.DamageCalculatorEngine;
using Abstractions.Shared.Events;
using Assets.Abstractions.RPG.Units;
using UnityEngine;

namespace Assets.Game.Scripts.Events
{
    public class DamageAfterHitEventArgs : BaseEventArgs<DamageAfterHitEventArgs>
    {
        public ICharacter attacker;
        public ICharacter defender;
        public HitResult hitResult;
        public DamageSource damageSource;

        public DamageAfterHitEventArgs(ICharacter attack, ICharacter defense, DamageSource source, HitResult hitResult)
        {
            this.damageSource = source;
            this.attacker = attack;
            this.defender = defense;
            this.hitResult = hitResult;

            Debug.Log("Hit: " + hitResult.Damage);
        }
    }
}
