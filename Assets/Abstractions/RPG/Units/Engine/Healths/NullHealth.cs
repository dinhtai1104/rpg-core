using Assets.Abstractions.RPG.Misc;
using System;

namespace Assets.Abstractions.RPG.Units.Engine.Healths
{
    public class NullHealth : BaseNullEngine, IHealth
    {
        public float CurrentHealth { set; get; }
        public float MaxHealth { set; get; }
        public float MinHealth { set; get; }
        public bool Invincible { set; get; }

        public float HealthPercentage => 1;

        public event OnHealthChange OnValueChanged;

        public void Damage(float damage, EDamageType type)
        {
        }

        public void Healing(float amount)
        {
        }

        public void Reset()
        {
        }

        public void SubscribeRecieveDamageEvent(Action<float, EDamageType> callback)
        {
        }
    }
}
