using Assets.Abstractions.RPG.Units;
using Assets.Abstractions.RPG.Units.Engine.Healths;
using UnityEngine;

namespace Assets.Abstractions.RPG.Items.UsableItems
{
    public class HealthPoisonItem : UseableItem
    {
        [SerializeField] private float _healthHeal;
        public float HealthHeal
        {
            get => _healthHeal;
            set => _healthHeal = value;
        }

        public override void Use(ICharacter character)
        {
            base.Use(character);
            // character add heal
            character.GetEngine<IHealth>().Healing(HealthHeal);
        }
    }
}
