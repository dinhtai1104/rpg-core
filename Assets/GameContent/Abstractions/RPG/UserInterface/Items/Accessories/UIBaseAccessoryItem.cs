using Assets.Abstractions.RPG.Items.Accessories;
using TMPro;
using UnityEngine;

namespace Assets.Abstractions.RPG.UserInterface.Items.Accessories
{
    public class UIBaseAccessoryItem : UIBaseItem<BaseAccessoryItem>
    {
        [SerializeField] private TextMeshProUGUI _levelText;

        protected override void SetData()
        {
            // SetLevel
            _levelText.SetText($"Lv {RuntimeItem.Level + 1}");

            // SetFrame
            var frame = ResourceManager.Get<Sprite>($"Icon/Rarity_{RuntimeItem.Rarity}");
            SetFrame(frame);
        }

    }
}
