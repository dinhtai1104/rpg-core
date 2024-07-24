using Assets.Abstractions.RPG.Attributes;
using Assets.Abstractions.RPG.Units;
using UnityEngine;

namespace Assets.Abstractions.RPG.Items.UsableItems
{
    public class StatsItem : UseableItem
    {
        [SerializeField] private AttributeModifierData _modifierData;

        public override void Use(ICharacter character)
        {
            base.Use(character);
            character.GetEngine<IAttributeGroup>().AddModifier(_modifierData.AttributeName, _modifierData.Modifier, this);
        }
    }
}
