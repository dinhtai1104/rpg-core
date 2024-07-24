using Assets.Abstractions.RPG.Inventory;
using Assets.Abstractions.RPG.Items;
using Assets.Abstractions.RPG.Items.UsableItems;
using Assets.Abstractions.RPG.Misc;
using Assets.Abstractions.Shared.Core;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Assets.Abstractions.RPG.Test
{
    public class Game : Architecture<Game>
    {
        public override bool InjectSceneLoadedDependencies => false;
        [SerializeField] private InventoryHandler inventoryHandler;

        protected override async UniTask OnInitialize()
        {
            await base.OnInitialize();
            inventoryHandler = new InventoryHandler();
        }

        [Button]
        public void AddUseable(EUseable usableType, int amount, float data)
        {
            BaseRuntimeItem baseRuntimeItem = null;
            switch (usableType)
            {
                case EUseable.HealthPoison:
                    baseRuntimeItem = new HealthPoisonItem(amount, data);
                    break;
                case EUseable.Exp:
                    break;
                case EUseable.Rune:
                    break;
                case EUseable.Stat:
                    break;
                case EUseable.Invincible:
                    baseRuntimeItem = new InvincibleItem(amount, data);
                    break;
            }

            if (baseRuntimeItem == null) return;
            inventoryHandler.Add(Misc.EInventoryPack.Consumable, baseRuntimeItem);
        }
    }
}
