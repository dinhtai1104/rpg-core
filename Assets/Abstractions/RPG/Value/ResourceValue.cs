using Assets.Abstractions.RPG.Attributes;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Abstractions.RPG.Value
{
    [Serializable]
    public class ResourceValue
    {
        private float _totalAmount;
        protected readonly AttributeData _gainCalculator;

        public float MaxValue { protected set; get; }

        public float TotalAmountFloat => _totalAmount;
        public int TotalAmount => Mathf.CeilToInt(_totalAmount);
        public IEnumerable<AttributeModifier> Modifiers => _gainCalculator.Modifiers;

        private readonly List<Action<float>> _listeners;

        public ResourceValue(float maxValue = float.MaxValue)
        {
            MaxValue = maxValue;
            _gainCalculator = new AttributeData(1f);
            _gainCalculator.SetConstraintMax(maxValue);
            _listeners = new List<Action<float>>();
        }

        public void SetConstrainMax(float max)
        {
            _gainCalculator.SetConstraintMax(max);
        }

        public void AddListener(Action<float> listener)
        {
            _listeners.Add(listener);
        }

        public void RemoveListenter(Action<float> listener)
        {
            _listeners.Remove(listener);
        }

        public void ClearListeners()
        {
            _listeners.Clear();
        }

        public void AddModifier(AttributeModifier modifier)
        {
            _gainCalculator.AddModifier(modifier);
        }

        public void RemoveModifier(AttributeModifier modifier)
        {
            _gainCalculator.RemoveModifier(modifier);
        }

        public void RemoveModifierFromSource(AttributeModifier modifier, object source)
        {
            _gainCalculator.RemoveAllModifiersFromSource(source);
        }

        public void ClearModifiers()
        {
            _gainCalculator.ClearModifiers();
        }

        public virtual void SetAmount(float amount, bool invokeEvent = true)
        {
            _totalAmount = Mathf.Clamp(amount, 0f, MaxValue);
            if (invokeEvent)
                InvokeValueChanged();
        }

        public virtual void Add(float amount, bool invokeEvent = true)
        {
            if (amount < 0f)
            {
                Debug.LogError("Added amount need to be greater than 0");
                return;
            }

            _gainCalculator.BaseValue = amount;
            _totalAmount = Mathf.Min(_totalAmount + _gainCalculator.Value, MaxValue);

            if (invokeEvent)
                InvokeValueChanged();
        }

        public virtual void Substract(float amount, bool invokeEvent = true)
        {
            if (amount < 0f)
            {
                Debug.LogError("Substracted amount need to be greater than 0");
                return;
            }

            _totalAmount = Mathf.Max(0f, _totalAmount - amount);

            if (invokeEvent)
                InvokeValueChanged();
        }

        public virtual float PredictAdd(float amount)
        {
            amount = _gainCalculator.Value * amount;
            return Mathf.Min(_totalAmount + amount, MaxValue);
        }

        private void InvokeValueChanged()
        {
            foreach (var listenter in _listeners)
            {
                listenter.Invoke(_totalAmount);
            }
        }
    }
}
