using Assets.Abstractions.RPG.Misc;
using System;

namespace Assets.Abstractions.RPG.Units.Engine.Healths
{
    public delegate void OnHealthChange(IHealth health);
    public interface IHealth : IEngine
    {
        event OnHealthChange OnValueChanged;
        float CurrentHealth { set; get; }
        float MaxHealth { set; get; }
        float MinHealth { set; get; }
        bool Invincible { set; get; }
        float HealthPercentage { get; }
        void Reset();
        void Damage(float damage, EDamageType type);
        void Healing(float amount);
        void SubscribeRecieveDamageEvent(Action<float, EDamageType> callback);
    }
}
