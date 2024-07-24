using Assets.Abstractions.RPG.Items;
using Assets.Abstractions.RPG.Items.Accessories;
using Assets.Abstractions.RPG.Items.UsableItems;
using Assets.Abstractions.RPG.Misc;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Abstractions.RPG.Inventory
{
    [System.Serializable]
    public class InventoryHandler
    {
        [ShowInInspector] private List<BaseAccessoryItem> _accessoryItems;
        [ShowInInspector] private List<UseableItem> _consumableItems;

        public InventoryHandler()
        {
            _accessoryItems = new();
            _consumableItems = new();
        }

        public void Drop(EInventoryPack pack, BaseRuntimeItem item) 
        {
            switch (pack)
            {
                case EInventoryPack.Accessory:
                    (item as BaseAccessoryItem).OnUnequip(); // ensure unequip before drop
                    _accessoryItems.Remove(item as BaseAccessoryItem);
                    break;
                case EInventoryPack.Consumable:
                    _consumableItems.Remove(item as UseableItem);
                    break;
            }
        }
        public void Add(EInventoryPack pack, BaseRuntimeItem item)
        {
            switch (pack)
            {
                case EInventoryPack.Accessory:
                    AddItem(item, ref _accessoryItems);
                    break;
                case EInventoryPack.Consumable:
                    AddItem(item, ref _consumableItems);
                    break;
            }
        }

        public void AddItem<T>(BaseRuntimeItem item, ref List<T> items) where T : BaseRuntimeItem
        {
            foreach (var itm in items)
            {
                if (itm.IsSame(item))
                {
                    itm.Add(itm);
                    return;
                }
            }
            items.Add(item as T);
        }
    }
}