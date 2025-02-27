using Assets.Abstractions.RPG.Misc;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Assets.Abstractions.RPG.Units.Engine.Healths
{
    [DisallowMultipleComponent]
    public class Health : BaseHealth
    {
        [SerializeField, MinValue(0f)] private float _maxHealth;

        [SerializeField] private float _currentHealth;

        [SerializeField] private bool _invincible;

        private bool _isZero;
        private bool _isFull;

        public override bool Invincible
        {
            set { _invincible = value; }
            get { return _invincible; }
        }

        public override void Damage(float damage, EDamageType type)
        {
            if (Invincible || CurrentHealth <= MinHealth)
                return;

            DamageHealth(DamageShieldDamageType(type, damage));
            InvokeDamageEvent(damage, type);
        }

        public override void Healing(float amount)
        {
            CurrentHealth += amount;
        }

        public override void Reset()
        {
            _isZero = false;
            _isFull = false;
            Initialized = false;
        }

        public override float MaxHealth
        {
            set
            {
                if (value > _maxHealth)
                {
                    float diff = value - _maxHealth;
                    _maxHealth = value;
                    CurrentHealth += diff;
                }
                else
                {
                    _maxHealth = value;
                }
            }
            get { return _maxHealth; }
        }

        public override float CurrentHealth
        {
            set
            {
                if (float.IsNaN(value))
                    return;

                _currentHealth = Mathf.Clamp(value, MinHP, _maxHealth);

                InvokeChangeValue();

                if (_currentHealth <= 0f)
                {
                    if (Initialized && !_isZero)
                    {
                        //Messenger.Broadcast(EventKey.HEALTH_ZERO, gameObject.GetInstanceID());
                    }

                    _isZero = true;
                    _currentHealth = 0f;
                }
                else if (_currentHealth >= _maxHealth)
                {
                    if (Initialized && !_isFull)
                    {
                        // Messenger.Broadcast(EventKey.HEALTH_FULL, gameObject.GetInstanceID());
                    }
                    _isFull = true;
                    _currentHealth = _maxHealth;
                }
                else if (_currentHealth > 0f && _currentHealth < _maxHealth)
                {
                    _isFull = false;
                    _isZero = false;
                }
            }
            get { return _currentHealth; }
        }

        public override float HealthPercentage => _currentHealth / _maxHealth;

        private void DamageHealth(float damage)
        {
            CurrentHealth = Mathf.Max(CurrentHealth - damage, MinHealth);
        }

        [Button]
        private void Kill()
        {
            CurrentHealth = 0;
        }
    }
}