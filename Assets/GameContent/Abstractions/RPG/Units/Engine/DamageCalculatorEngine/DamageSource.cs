using Assets.Abstractions.RPG.Attributes;
using Assets.Abstractions.RPG.Misc;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Abstractions.RPG.Units.Engine.DamageCalculatorEngine
{
    [Serializable, HideLabel, InlineProperty]
    public class DamageSource
    {
        [SerializeField] private AttributeData m_Damage;

        [SerializeField, Range(0f, 1f)] private float m_DamageHealthPercentage;

        [SerializeField] private EDamageType m_Type;


        [SerializeField] private bool m_CannotEvade = false;

        [SerializeField] private bool m_CannotHurt = false;

        [SerializeField] private bool m_CannotCrit = false;

        private GameObject m_AttackPoint;

        public float Value
        {
            set { m_Damage.BaseValue = value; m_Damage.RecalculateValue(); }
            get { return m_Damage.Value; }
        }

        public void AddModifier(AttributeModifier mod)
        {
            m_Damage.AddModifier(mod);
        }

        public void RemoveModifier(AttributeModifier mod)
        {
            m_Damage.RemoveModifier(mod);
        }

        public void ClearModifiers()
        {
            m_Damage.ClearModifiers();
        }

        public float DamageHealthPercentage
        {
            set { m_DamageHealthPercentage = value; }
            get { return m_DamageHealthPercentage; }
        }

        public EDamageType Type
        {
            set { m_Type = value; }
            get { return m_Type; }
        }

        public bool CannotEvade
        {
            set { m_CannotEvade = value; }
            get { return m_CannotEvade; }
        }

        public bool CannotHurt
        {
            set { m_CannotHurt = value; }
            get { return m_CannotHurt; }
        }

        public bool CannotCrit
        {
            set { m_CannotCrit = value; }
            get { return m_CannotCrit; }
        }

        public GameObject AttackPoint
        {
            get => m_AttackPoint;
            set => m_AttackPoint = value;
        }

        public DamageSource()
        {
            m_Damage = new AttributeData();
        }
    }
}
