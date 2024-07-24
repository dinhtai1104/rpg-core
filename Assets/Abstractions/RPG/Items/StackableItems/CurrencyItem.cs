using Assets.Abstractions.RPG.Items.UsableItems;
using Assets.Abstractions.RPG.Misc;
using Assets.Abstractions.RPG.Value;

namespace Assets.Abstractions.RPG.Items.StackableItems
{
    [System.Serializable]
    public class CurrencyItem : StackableItem
    {
        private ECurrency _currencyType;
        public ECurrency CurrencyType { get => _currencyType; set => _currencyType = value; }



        public override bool IsSame(BaseRuntimeItem other)
        {
            return base.IsSame(other) && (other as CurrencyItem).CurrencyType == CurrencyType;
        }
    }
}
