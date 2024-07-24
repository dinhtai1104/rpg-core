using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Globalization;
using Assets.Abstractions.RPG.Items;

namespace Assets.Abstractions.RPG.Attributes
{
    public class StatAffix : BaseAffix
    {
        private readonly string _statName;
        private readonly AttributeModifier _modifier;
        private IAttributeGroup _stats;

        public string StatName => _statName;
        public float Value => _modifier.Value;
        public AttributeModType ModifierType => _modifier.Type;
        public AttributeModifier Modifier => _modifier;
        public override string AffixDescription { get; protected set; }


        public override IList<string> SerializedValues => serializedValue;
        [ShowInInspector] private List<string> serializedValue;

        public StatAffix(int id, string descKey, string statName, AttributeModType modType, float value) : base(id, descKey)
        {
            _statName = statName;
            _modifier = new AttributeModifier(value, modType, this);
            serializedValue = new List<string>() { value.ToString(CultureInfo.InvariantCulture) };
        }

        public override object Clone()
        {
            var clone = (StatAffix)base.Clone();
            clone.serializedValue = serializedValue.ToList();
            return clone;
        }

        public override void OnEquip(IAttributeGroup stats)
        {
            base.OnEquip(stats);
            _stats = stats;
            stats.AddModifier(_statName, _modifier, this);
        }

        public override void OnUnequip()
        {
            base.OnUnequip();
            _stats?.RemoveModifier(_statName, _modifier);
        }

        public override void OnRemoveFromItem(BaseRuntimeItem item)
        {
            base.OnRemoveFromItem(item);
            _stats?.RemoveModifier(_statName, _modifier);
        }

        public override string ToString()
        {
            return $"{nameof(StatName)}: {StatName}, {AffixDescription}, {nameof(_modifier)}: {_modifier}";
        }
    }
}
