using System;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Assets.Abstractions.RPG.Attributes
{
    [Serializable]
    public class AttributeModifierData
    {
        [SerializeField] private string _attributeName;

        [SerializeField, InlineProperty, HideLabel] private AttributeModifier _modifier;

        public string AttributeName => _attributeName;
        public AttributeModifier Modifier => _modifier;
    }
}
