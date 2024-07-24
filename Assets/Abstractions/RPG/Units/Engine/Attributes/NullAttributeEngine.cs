using Assets.Abstractions.RPG.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Abstractions.RPG.Units.Engine.Attributes
{
    public class NullAttributeEngine : BaseNullEngine, IAttributeGroup
    {
        public IEnumerable<string> StatNames => new List<string>();

        public void AddListener(string statName, Action<float> callback)
        {
        }

        public void AddModifier(string statName, AttributeModifier mod, object source)
        {
        }

        public void AddStat(string statName, float baseValue, float min = 0, float max = float.MaxValue)
        {
        }

        public void CalculateStats()
        {
        }

        public void ClearAllModifiers()
        {
        }

        public void Copy(IAttributeGroup statGroup, float percentage = 1)
        {
        }

        public void Copy(IAttributeGroup statGroup, string[] _ignoreKeys, float percentage = 1)
        {
        }

        public int CountModifierFromSource(string statName, object source)
        {
            return 0;
        }

        public float GetBaseValue(string statName, float defaultValue = 0)
        {
            return 0;
        }

        public float GetLastValue(string statName, float defaultValue = 0)
        {
            return 0;
        }

        public float GetMaxConstraint(string statName)
        {
            return 0;
        }

        public float GetMinConstraint(string statName)
        {
            return 0;
        }

        public IEnumerable<AttributeModifier> GetModifiers(string statName)
        {
            return null;
        }

        public float GetValue(string statName, float defaultValue = 0)
        {
            return 0;
        }

        public bool HasModifier(string statName, AttributeModifier modifier)
        {
            return false;
        }

        public bool HasStat(string statName)
        {
            return false;
        }

        public void RemoveListener(string statName, Action<float> callback)
        {
        }

        public void RemoveModifier(string statName, AttributeModifier mod)
        {
        }

        public void RemoveModifiersFromSource(object source)
        {
        }

        public IAttributeGroup SetBaseValue(string statName, float value, bool callUpdater = true)
        {
            return this;
        }

        public IAttributeGroup SetMaxValue(string statName, float max)
        {
            return this;
        }

        public IAttributeGroup SetMinValue(string statName, float min)
        {
            return this;
        }
    }
}
