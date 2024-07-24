using Assets.Abstractions.RPG.Items;
using Assets.Abstractions.RPG.Items.Accessories;
using Assets.Abstractions.RPG.Items.UsableItems;
using Assets.Abstractions.RPG.Manager;
using Assets.Abstractions.RPG.Misc;
using Assets.Abstractions.RPG.Test;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
namespace Assets.Abstractions.RPG.UserInterface.Items
{
    public class UISlotItem : UIElement
    {
        [SerializeField] private RectTransform _itemHolder;
        [SerializeField] private TextMeshProUGUI _amountItemText;
        private UIBaseItem _itemUI;

        private BaseRuntimeItem runtimeItem;
        public RectTransform ItemHolder { get => _itemHolder; }
        public UIBaseItem ItemUI { get => _itemUI; }
        public BaseRuntimeItem RuntimeItem { get => runtimeItem; set => runtimeItem = value; }

        public void SetData(BaseRuntimeItem runtimeItem)
        {
            this.runtimeItem = runtimeItem;
            if (_itemUI != null)
            {
                // Destroy
                Destroy(_itemUI.gameObject);
            }

            var resourceManager = Game.Instance.GetService<IResourceManager>();
            _itemUI = Instantiate(resourceManager.Get<UIBaseItem>($"UI/Items/{runtimeItem.PathUIPrefab}"), ItemHolder);
            _itemUI.SetData(runtimeItem, this);
        }

        public void SetAmount(int amount)
        {
            if (amount == 0)
            {
                this._amountItemText.SetText(string.Empty);
                return;
            }
            this._amountItemText.SetText(amount.ToString());
        }

        [Button]
        public void Test()
        {
            // Create Health
            //var healPoison = new HealthPoisonItem(100, 10);
            //healPoison.Icon = "HealthPoison";

            //SetData(healPoison);

            var equipmentItem = new BaseAccessoryItem();
            equipmentItem.Level = 10;
            equipmentItem.Rarity = ERarity.Common;
            equipmentItem.Icon = "0";

            SetData(equipmentItem);
        }
    }
}
