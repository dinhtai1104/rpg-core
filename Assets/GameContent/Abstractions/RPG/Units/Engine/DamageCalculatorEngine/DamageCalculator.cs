using Assets.Abstractions.RPG.Attributes;
using Assets.Abstractions.RPG.Misc;
using Assets.Abstractions.RPG.Units;
using Assets.Abstractions.RPG.Units.Engine.Healths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Abstractions.RPG.Units.Engine.DamageCalculatorEngine
{
    public class DamageCalculator : BaseMonoEngine, IDamageCalculator
    {
        private readonly Dictionary<EDamageType, bool> m_ImmuneDict = new Dictionary<EDamageType, bool>();
        private readonly DamageDealer m_ReflectDamageDealer = new DamageDealer();
        private const float MinDamage = 1f;
        private readonly TemporaryModifier m_AttackerTempMods = new TemporaryModifier();
        private readonly TemporaryModifier m_DefenderTempMods = new TemporaryModifier();


        public void SetImmuneDamageType(EDamageType type, bool immune)
        {
            if (!m_ImmuneDict.ContainsKey(type))
            {
                m_ImmuneDict.Add(type, false);
            }

            m_ImmuneDict[type] = immune;
        }

        public bool IsImmuneDamageType(EDamageType type)
        {
            if (m_ImmuneDict.ContainsKey(type)) return m_ImmuneDict[type];
            m_ImmuneDict.Add(type, false);
            return false;
        }

        public HitResult CalculateDamage(ICharacter defender, ICharacter attacker, DamageSource source)
        {
            if (defender.GetEngine<IHealth>().Invincible)
            {
                return HitResult.InvincibleHitResult;
            }

            var success = true;
            var crit = false;
            var lastHit = false;
            var hurt = false;
            var evade = false;
            var block = false;

            float damage;
            bool immune = IsImmuneDamageType(source.Type);

            if (immune)
            {
                // FireEvent Hit Immune

                return HitResult.InvincibleHitResult;
            }

            // FireEvent BeforeHit
            //GameCore.Event.Fire(this, DamageBeforeHitEventArgs.Create(attacker, defender, source));
            damage = CalculatePhysicalDamage(defender, attacker, source, out success, out crit, out hurt, out evade, out block);

            // Clear temporary mods
            m_AttackerTempMods.Clear(attacker.GetEngine<IAttributeGroup>());
            m_DefenderTempMods.Clear(defender.GetEngine<IAttributeGroup>());




            return new();
        }

        private float CalculateRawDamage(DamageSource source, ICharacter defender)
        {
            return source.Value + defender.GetEngine<IHealth>().MaxHealth * source.DamageHealthPercentage;
        }

        private float CalculatePhysicalDamage(ICharacter defender, ICharacter attacker, DamageSource source, out bool success,
            out bool crit, out bool hurt,
            out bool evade, out bool block)
        {
            success = true;
            crit = false;
            hurt = false;
            evade = false;
            block = false;

            return 0;
        }

        private float CalculateMagicDamage(ICharacter defender, ICharacter attacker, DamageSource source)
        {
            return 0;
        }

        public void AddAttackerTemporaryModifier(string modName, AttributeModifier mod)
        {
            m_AttackerTempMods.AddModifier(modName, mod);
        }

        public void AddDefenderTemporaryModifier(string modName, AttributeModifier mod)
        {
            m_DefenderTempMods.AddModifier(modName, mod);
        }


        #region Private classes

        private class TemporaryModifier
        {
            private readonly Dictionary<string, AttributeModifier> m_TempDict;

            public TemporaryModifier()
            {
                m_TempDict = new Dictionary<string, AttributeModifier>();
            }

            public void AddModifier(string name, AttributeModifier mod)
            {
                if (!m_TempDict.ContainsKey(name))
                {
                    m_TempDict.Add(name, mod);
                }
            }

            public void ApplyModifiers(IAttributeGroup stat)
            {
                foreach (var key in m_TempDict.Keys)
                {
                    stat.AddModifier(key, m_TempDict[key], this);
                }
            }

            public void Clear(IAttributeGroup stat)
            {
                stat.RemoveModifiersFromSource(this);
                m_TempDict.Clear();
            }
        }

        #endregion
    }
}
