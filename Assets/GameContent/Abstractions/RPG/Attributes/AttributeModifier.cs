using UnityEngine;
using Sirenix.OdinInspector;
using System;
namespace Assets.Abstractions.RPG.Attributes
{
    [System.Serializable]
    public class AttributeModifier
    {
        public virtual bool IsDirectValue => true;
        [SerializeField, HideInInspector] private float _value;

        [SerializeField] private AttributeModType _type;

        [ShowInInspector]
        public virtual float Value
        {
            set
            {
                _value = value;
                _recalculateValueAction?.Invoke();
            }
            get => _value;
        }

        // Same as Stat Recalculate value.
        // Attach Stat to statmodifier cause loop serialization
        // Cant debug 
        private Action _recalculateValueAction;

        public AttributeModType Type => _type;

        [HideInInspector] public int Order;
        [HideInInspector] public object Source;

        [ShowInInspector]
        public string SourceString
        {
            get
            {
                return Source.GetType().ToString() + " " + Source?.ToString();
            }
            set { }
        }

        public Action RecalculateValueAction
        {
            set => _recalculateValueAction = value;
        }

        public AttributeModifier(float value, AttributeModType type, int order, object source)
        {
            Value = value;
            _type = type;
            Order = order;
            Source = source;
        }

        public AttributeModifier(float value, AttributeModType type, object source) : this(value, type, (int)type, source)
        {
        }

        public static AttributeModType StringToType(string modType)
        {
            switch (modType)
            {
                case "Flat": return AttributeModType.Flat;
                case "PercentAdd": return AttributeModType.PercentAdd;
                case "PercentMult": return AttributeModType.PercentMult;
            }

            return AttributeModType.Flat;
        }

        public override string ToString()
        {
            return $"{nameof(_value)}: {_value}, {nameof(_type)}: {_type}, {nameof(Order)}: {Order}";
        }

        public AttributeModifier Clone()
        {
            return new AttributeModifier(_value, _type, Source);
        }

        public virtual float GetFinalValue()
        {
            return Value;
        }
    }
}
