using Assets.Abstractions.RPG.Attributes;
using Assets.Abstractions.RPG.Misc;
using Assets.Abstractions.RPG.Units;
using UnityEngine;

namespace Assets.Abstractions.RPG.Items.UsableItems
{
    [System.Serializable]
    public class StatsItem : UsableItem
    {
        public override EUseable UseableType => EUseable.Stat;
        [SerializeField] private AttributeModifierData _modifierData;

        public StatsItem(int amount, float data) : base(amount, data)
        {
        }

        public override void Use(ICharacter character)
        {
            base.Use(character);
            character.GetEngine<IAttributeGroup>().AddModifier(_modifierData.AttributeName, _modifierData.Modifier, this);
        }
        public override bool IsSame(BaseRuntimeItem other)
        {
            return base.IsSame(other) && _modifierData.Equals(_modifierData);
        }
    }
}
