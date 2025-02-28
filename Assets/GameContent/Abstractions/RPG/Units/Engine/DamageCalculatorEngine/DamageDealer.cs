using Assets.Abstractions.RPG.Attributes;
using Assets.Abstractions.RPG.Units;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Abstractions.RPG.Units.Engine.DamageCalculatorEngine
{
    [Serializable, HideLabel, FoldoutGroup("Damage Dealer")]
    public class DamageDealer : IDamageDealer
    {
        [SerializeField] private DamageSource m_Source;

        [SerializeField] private bool m_ApplyEffect = true;

        public DamageSource DamageSource => m_Source;

        public CharacterActor Owner { set; get; }

        public DamageDealer()
        {
            m_Source = new DamageSource();
        }

        public void Init(IAttributeGroup stat)
        {
            stat.AddListener(AttributeKey.Damage, OnUpdateBaseDamage);
        }

        public void Release(IAttributeGroup stat)
        {
            stat.RemoveListener(AttributeKey.Damage, OnUpdateBaseDamage);
        }

        private void OnUpdateBaseDamage(float value)
        {
            m_Source.Value = value;
        }

        public HitResult DealDamage(CharacterActor attacker, CharacterActor defender)
        {
            if (defender.IsDead)
            {
                return HitResult.FailedResult;
            }



            HitResult hitResult = defender.GetEngine<IDamageCalculator>().CalculateDamage(defender, attacker, m_Source);
            return hitResult;
        }

        public void CopyData(DamageDealer damageDealer)
        {
            m_Source.Value = damageDealer.DamageSource.Value;
            m_Source.Type = damageDealer.DamageSource.Type;
            m_Source.DamageHealthPercentage = damageDealer.DamageSource.DamageHealthPercentage;
            m_Source.CannotEvade = damageDealer.DamageSource.CannotEvade;
            m_Source.CannotHurt = damageDealer.DamageSource.CannotHurt;
            Owner = damageDealer.Owner;
        }
    }
}
