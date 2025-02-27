using Assets.Abstractions.RPG.Misc;
using Assets.Abstractions.RPG.Units;
using Assets.Abstractions.RPG.Units.Engine.Healths;
using UnityEngine;

namespace Assets.Abstractions.RPG.Items.UsableItems
{
    [System.Serializable]
    public class HealthPoisonItem : UsableItem
    {
        public override EUseable UseableType => EUseable.HealthPoison;
        [SerializeField] private float _healthHeal;
        public float HealthHeal
        {
            get => _healthHeal;
            set => _healthHeal = value;
        }

        public HealthPoisonItem(int amount, float healthHeal) : base(amount, healthHeal)
        {
            _healthHeal = healthHeal;
        }

        public override void Use(ICharacter character)
        {
            base.Use(character);
            // character add heal
            character.GetEngine<IHealth>().Healing(HealthHeal);
        }

        public override bool IsSame(BaseRuntimeItem other)
        {
            return base.IsSame(other) && _healthHeal == (other as HealthPoisonItem)._healthHeal;
        }
    }
}
