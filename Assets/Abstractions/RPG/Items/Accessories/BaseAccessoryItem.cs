using Assets.Abstractions.RPG.Attributes;
using Assets.Abstractions.RPG.Units.Equipment;

namespace Assets.Abstractions.RPG.Items.Accessories
{
    public class BaseAccessoryItem : BaseRuntimeItem
    {
        private AttributeData _mainAttribute;
        private SlotType _slotType;
        private bool _isEquipped;

        public AttributeData MainAttribute { get => _mainAttribute; set => _mainAttribute = value; }
        public SlotType Slot { get => _slotType; set => _slotType = value; }
        public bool IsEquipped { get => _isEquipped; set => _isEquipped = value; }
        public virtual void OnEquip(IAttributeGroup attributeGroup) { }
        public virtual void OnUnequip() { }
    }
}
