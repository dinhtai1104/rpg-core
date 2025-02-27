using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Assets.Abstractions.RPG.Misc;

namespace Assets.Abstractions.RPG.Units.Engine.Healths
{
    public abstract class BaseHealth : BaseMonoEngine, IHealth
    {
        [SerializeField, Range(0f, 100000f)] private float _minHealth;
        public float MinHP => _minHealth;

        public event OnHealthChange OnValueChanged;
        public bool Initialized { set; get; }
        public abstract float CurrentHealth { set; get; }
        public abstract float MaxHealth { set; get; }
        public float MinHealth { get; set; }
        public abstract bool Invincible { set; get; }
        public abstract float HealthPercentage { get; }

        private Dictionary<EDamageType, float> _shieldDict;
        private List<Action<float, EDamageType>> _onRecieveDamage;

        public abstract void Damage(float amount, EDamageType type);
        public abstract void Healing(float amount);

        public void SubscribeRecieveDamageEvent(Action<float, EDamageType> callback)
        {
            _onRecieveDamage.Add(callback);
        }

        protected void InvokeDamageEvent(float damage, EDamageType type)
        {
            foreach (var callback in _onRecieveDamage)
            {
                callback.Invoke(damage, type);
            }
        }

        public abstract void Reset();


        public override void Initialize()
        {
            base.Initialize();
            var damageTypes = (EDamageType[])Enum.GetValues(typeof(EDamageType));
            _shieldDict = new Dictionary<EDamageType, float>(damageTypes.Length);
            _onRecieveDamage = new List<Action<float, EDamageType>>();

            foreach (var t in damageTypes)
            {
                _shieldDict.Add(t, 0f);
            }
        }

        public void AddShieldDamageType(EDamageType damageType, float amount)
        {
            _shieldDict[damageType] += amount;
        }

        public void SetShieldDamageType(EDamageType damageType, float amount)
        {
            _shieldDict[damageType] = amount;
        }

        public float GetShieldDamageType(EDamageType damageType)
        {
            return _shieldDict[damageType];
        }

        protected void InvokeChangeValue()
        {
            OnValueChanged?.Invoke(this);
        }

        protected float DamageShieldDamageType(EDamageType damageType, float damage)
        {
            var currentShield = _shieldDict[damageType];

            if (currentShield <= 0f)
            {
                return damage;
            }

            if (currentShield > damage)
            {
                currentShield -= damage;
                _shieldDict[damageType] = currentShield;
                return 0f;
            }

            var remainingDamage = damage - currentShield;
            currentShield = 0f;
            _shieldDict[damageType] = currentShield;
            return remainingDamage;
        }
    }
}