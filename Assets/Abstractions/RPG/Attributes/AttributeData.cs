using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
namespace Assets.Abstractions.RPG.Attributes
{
    [System.Serializable]
    public class AttributeData
    {
        [SerializeField] private float _baseValue;

        public float BaseValue
        {
            set
            {
                value = Mathf.Clamp(value, _minValue, _maxValue);

                if (System.Math.Abs(_baseValue - value) > Mathf.Epsilon)
                {
                    _baseValue = value;
                    RecalculateValue();
                }
            }
            get { return _baseValue; }
        }

        [SerializeField, ReadOnly] private float _value;
        [SerializeField] private float _minValue = 0f;
        [SerializeField] private float _maxValue = float.MaxValue;

        public float LastValue { private set; get; }
        public float Value => _value;
        public float ConstraintMin => _minValue;
        public float ConstraintMax => _maxValue;

        public IEnumerable<AttributeModifier> Modifiers => attributeModifiers;

        [SerializeField] private List<AttributeModifier> attributeModifiers;

        private float _lastValue;
        private readonly List<Action<float>> _listeners;

        public AttributeData()
        {
            attributeModifiers = new List<AttributeModifier>();
            _listeners = new List<Action<float>>();
        }

        public AttributeData(float baseValue) : this()
        {
            BaseValue = baseValue;
        }

        public AttributeData(float baseValue, float min, float max) : this(baseValue)
        {
            SetConstraintMin(min);
            SetConstraintMax(max);
        }

        public void SetConstraintMin(float min)
        {
            if (min > _maxValue)
            {
                Debug.LogError("Min value " + min + " cannot be greater than Max value  " + _maxValue);
                return;
            }

            _minValue = min;
        }

        public void SetConstraintMax(float max)
        {
            if (max < _minValue)
            {
                Debug.LogError("Max value " + max + " cannot be smaller than Min value  " + _minValue);
                return;
            }

            _maxValue = max;
        }

        public void AddListener(Action<float> callback)
        {
            _listeners.Add(callback);
            callback(Value);
        }

        public void RemoveListener(Action<float> callback)
        {
            _listeners.Remove(callback);
        }

        public void ClearAllListeners()
        {
            _listeners.Clear();
        }

        public virtual AttributeModifier GetModifier(int index)
        {
            return attributeModifiers[index];
        }

        public virtual void AddModifier(AttributeModifier mod)
        {
#if UNITY_EDITOR
            if (attributeModifiers.Contains(mod))
                Debug.LogWarning("Duplicate mod: " + mod);
#endif

            mod.RecalculateValueAction = RecalculateValue;
            attributeModifiers.Add(mod);
            attributeModifiers.Sort(CompareModifierOrder);
            RecalculateValue();
        }

        public virtual bool RemoveModifier(AttributeModifier mod)
        {
            if (!attributeModifiers.Remove(mod)) return false;
            RecalculateValue();
            return true;
        }

        public bool HasModifier(AttributeModifier modifier)
        {
            return attributeModifiers.Contains(modifier);
        }

        public void ClearModifiers()
        {
            attributeModifiers.Clear();
            RecalculateValue();
        }

        public virtual bool RemoveAllModifiersFromSource(object source)
        {
            var didRemove = false;

            for (int i = attributeModifiers.Count - 1; i >= 0; i--)
            {
                if (attributeModifiers[i].Source != source) continue;
                didRemove = true;
                attributeModifiers.RemoveAt(i);
            }

            RecalculateValue();
            return didRemove;
        }

        public virtual int CountModifiersFromSource(object source)
        {
            var count = 0;
            for (int i = attributeModifiers.Count - 1; i >= 0; i--)
            {
                if (attributeModifiers[i].Source == source) count++;
            }

            return count;
        }

        protected virtual int CompareModifierOrder(AttributeModifier a, AttributeModifier b)
        {
            if (a.Order < b.Order)
                return -1;
            else if (a.Order > b.Order) return 1;
            return 0; //if (a.Order == b.Order)
        }

        /// <summary>
        /// Follow formula: Value = Sum(Flat) x (1 + Sum(Increase) - Sum(Reduce)) x Product(1 + More) x Product(1 - Less)
        /// </summary>
        /// <param name="baseValue"></param>
        /// <returns></returns>
        protected virtual float CalculateFinalValue(float baseValue)
        {
            float finalValue = baseValue;
            float sumPercentAdd = 0;

            for (int i = 0; i < attributeModifiers.Count; i++)
            {
                AttributeModifier mod = attributeModifiers[i];

                switch (mod.Type)
                {
                    case AttributeModType.Flat:
                        finalValue += mod.GetFinalValue();
                        break;
                    case AttributeModType.PercentAdd:
                        {
                            sumPercentAdd += mod.GetFinalValue();

                            if (i + 1 >= attributeModifiers.Count || attributeModifiers[i + 1].Type != AttributeModType.PercentAdd)
                            {
                                finalValue *= 1 + sumPercentAdd;
                                sumPercentAdd = 0;
                            }

                            break;
                        }

                    case AttributeModType.PercentMult:
                        finalValue *= 1 + mod.GetFinalValue();
                        break;
                }
            }

            // Workaround for float calculation errors, like displaying 12.00002 instead of 12
            return (float)System.Math.Round(finalValue, 2);
        }

        /// <summary>
        /// Follow formula: Value = Sum(Flat) x (1 + Sum(Increase) - Sum(Reduce)) x Sum(1 + More) x Sum(1 - Less)
        /// </summary>
        /// <param name="baseValue"></param>
        /// <returns></returns>
        protected virtual float CalculateFinalValuePOE(float baseValue)
        {
            var finalValue = baseValue;
            var sumPercentAdd = 0f;
            var sumPercentMulMore = 0f;
            var sumPercentMulLess = 0f;

            for (int i = 0; i < attributeModifiers.Count; i++)
            {
                AttributeModifier mod = attributeModifiers[i];

                switch (mod.Type)
                {
                    case AttributeModType.Flat:
                        finalValue += mod.GetFinalValue();
                        break;
                    case AttributeModType.PercentAdd:
                        {
                            sumPercentAdd += mod.GetFinalValue();
                            break;
                        }

                    case AttributeModType.PercentMult:

                        // More
                        if (mod.Value >= 0f)
                        {
                            sumPercentMulMore += mod.GetFinalValue();
                        }
                        // Less
                        else
                        {
                            sumPercentMulLess += mod.GetFinalValue();
                        }

                        break;
                }
            }

            // Percent Add (Increase, Decrease)
            finalValue *= 1f + sumPercentAdd;

            // Percent Mul (More, Less)
            finalValue *= 1f + sumPercentMulMore;
            finalValue *= 1f + sumPercentMulLess;

            // Workaround for float calculation errors, like displaying 12.00002 instead of 12
            return finalValue;
            //            return (float) System.Math.Round(finalValue, 4);
        }

        public void RecalculateValue()
        {
            //            lastBaseValue = _baseValue;
            LastValue = _value;
            float baseValue = _baseValue;
            _value = attributeModifiers != null ? CalculateFinalValuePOE(baseValue) : baseValue;
            _value = Mathf.Clamp(_value, _minValue, _maxValue);

            if (System.Math.Abs(_value - _lastValue) > 1e-4)
            {
                _lastValue = _value;
                InvokeListeners();
            }
        }

        public float CalculateTemporaryValue(float baseValue)
        {
            float value = CalculateFinalValuePOE(baseValue);
            value = Mathf.Clamp(value, _minValue, _maxValue);
            return value;
        }

        private void InvokeListeners()
        {
            foreach (var listener in _listeners)
            {
                listener?.Invoke(_value);
            }
        }

        public override string ToString()
        {
            return
                $"{nameof(_baseValue)}: {_baseValue}, {nameof(_value)}: {_value}, {nameof(_minValue)}: {_minValue}, {nameof(_maxValue)}: {_maxValue}, {nameof(attributeModifiers)} count: {attributeModifiers.Count}";
        }
    }
}
