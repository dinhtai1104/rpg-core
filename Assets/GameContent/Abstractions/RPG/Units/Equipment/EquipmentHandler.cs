using Assets.Abstractions.RPG.Items.Accessories;
using Assets.Abstractions.RPG.Attributes;
using Assets.Abstractions.RPG.Misc;

using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;

namespace Assets.Abstractions.RPG.Units.Equipment
{
    public delegate void OnEquipItemDelegate(BaseAccessoryItem item);
    public delegate void OnUnequipItemDelegate(BaseAccessoryItem item);
    [System.Serializable]
    public class EquipmentHandler
    {
        private readonly CharacterType _characterType;
        private readonly IAttributeGroup _attributes;
        [ShowInInspector]
        protected readonly Dictionary<SlotType, EquipmentSlot> _equipmentSlots;
        [ShowInInspector]
        private readonly List<BaseAccessoryItem> _equipments;

        public IEnumerable<BaseAccessoryItem> EquipedItems => _equipments;
        public event OnEquipItemDelegate OnEquipItem;

        public EquipmentHandler(CharacterType characterType, IAttributeGroup stats)
        {
            _characterType = characterType;
            _attributes = stats;
            _equipmentSlots = new Dictionary<SlotType, EquipmentSlot>(EnumCollection.SlotTypes.Count);
            _equipments = new List<BaseAccessoryItem>(EnumCollection.SlotTypes.Count);

            foreach (var slotType in EnumCollection.SlotTypes)
            {
                var equipmentSlot = new EquipmentSlot();
                equipmentSlot.SetUsable(_attributes, true);
                _equipmentSlots.Add(slotType, equipmentSlot);
            }
        }

        public bool IsEquipped(BaseAccessoryItem item)
        {
            if (!_equipmentSlots.ContainsKey(item.Slot)) return false;
            return _equipmentSlots[item.Slot].AccessoryItem == item;
        }

        public void Equip(BaseAccessoryItem item)
        {
            if (item == null)
            {
                Debug.LogWarning("Equip null Item");
                return;
            }

            if (_equipmentSlots.ContainsKey(item.Slot))
            {
                BaseAccessoryItem currentItem = _equipmentSlots[item.Slot].AccessoryItem;

                if (currentItem != null)
                {
                    Unequip(currentItem.Slot);
                }
            }


            item.IsEquipped = true;
            _equipments.Add(item);
            item.OnEquip(_attributes);
            _equipmentSlots[item.Slot].AccessoryItem = item;
            OnEquipItem?.Invoke(item);
        }

        public void Unequip(SlotType slot)
        {
            if (!_equipmentSlots.ContainsKey(slot)) return;

            EquipmentSlot equipmentSlot = _equipmentSlots[slot];

            if (equipmentSlot.AccessoryItem == null) return;

            BaseAccessoryItem item = equipmentSlot.AccessoryItem;
            item.IsEquipped = false;
            _equipments.Remove(item);
            item.OnUnequip();
            equipmentSlot.AccessoryItem = null;
        }

        public void UnequipAll()
        {
            foreach (var equipmentSlot in _equipmentSlots.Keys)
            {
                Unequip(equipmentSlot);
            }
            _equipments.Clear();
        }

        public void ApplyAllAffixes()
        {
            foreach (var equipableItem in _equipments)
            {
                equipableItem.OnEquip(_attributes);
            }
        }

        public void RemoveAllAffixes()
        {
            foreach (var equipableItem in _equipments)
            {
                equipableItem.OnUnequip();
            }
        }

        public BaseAccessoryItem GetEquipableItem(SlotType slot)
        {
            if (!_equipmentSlots.ContainsKey(slot))
            {
                //                Debug.LogError("There is no slot type " + slot);
                return null;
            }

            return _equipmentSlots[slot].AccessoryItem;
        }

        public bool IsSlotUsable(SlotType slotType)
        {
            return _equipmentSlots.ContainsKey(slotType) && _equipmentSlots[slotType].Usable;
        }

        public void SetSlotUsable(SlotType slotType, bool usable)
        {
            if (!_equipmentSlots.ContainsKey(slotType)) return;
            _equipmentSlots[slotType].SetUsable(_attributes, usable);
        }

        public bool HasEquippedItem(SlotType slotType)
        {
            return _equipmentSlots.ContainsKey(slotType) && _equipmentSlots[slotType].AccessoryItem != null;
        }

        [System.Serializable]
        protected class EquipmentSlot
        {
            [ShowInInspector]
            public bool Usable { private set; get; }
            [ShowInInspector]
            public BaseAccessoryItem AccessoryItem;

            public void SetUsable(IAttributeGroup statGroup, bool usable)
            {
                if (AccessoryItem != null)
                {
                    if (!Usable && usable)
                    {
                        AccessoryItem.OnEquip(statGroup);
                    }

                    if (Usable && !usable)
                    {
                        AccessoryItem.OnUnequip();
                    }
                }

                Usable = usable;
            }
        }
    }
}
