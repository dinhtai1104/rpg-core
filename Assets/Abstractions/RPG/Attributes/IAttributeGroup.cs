using System;
using System.Collections.Generic;

namespace Assets.Abstractions.RPG.Attributes
{
    public interface IAttributeGroup
    {
        IEnumerable<string> StatNames { get; }

        void AddStat(string statName, float baseValue, float min = 0f, float max = float.MaxValue);
        void AddListener(string statName, Action<float> callback);
        void RemoveListener(string statName, Action<float> callback);

        IAttributeGroup SetBaseValue(string statName, float value, bool callUpdater = true);
        IAttributeGroup SetMinValue(string statName, float min);
        IAttributeGroup SetMaxValue(string statName, float max);
        float GetBaseValue(string statName, float defaultValue = 0f);
        float GetValue(string statName, float defaultValue = 0f);
        float GetLastValue(string statName, float defaultValue = 0f);
        float GetMinConstraint(string statName);
        float GetMaxConstraint(string statName);
        IEnumerable<AttributeModifier> GetModifiers(string statName);

        void AddModifier(string statName, AttributeModifier mod, object source);
        void RemoveModifier(string statName, AttributeModifier mod);
        void RemoveModifiersFromSource(object source);
        void ClearAllModifiers();
        int CountModifierFromSource(string statName, object source);

        bool HasStat(string statName);
        bool HasModifier(string statName, AttributeModifier modifier);

        void Copy(IAttributeGroup statGroup, float percentage = 1f);
        void Copy(IAttributeGroup statGroup, string[] _ignoreKeys, float percentage = 1f);
        void CalculateStats();
    }
}
