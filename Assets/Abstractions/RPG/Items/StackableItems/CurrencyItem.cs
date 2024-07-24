using Assets.Abstractions.RPG.Value;

namespace Assets.Abstractions.RPG.Items.StackableItems
{
    [System.Serializable]
    public class CurrencyItem : StackableItem
    {
        private ResourceValue _resourceValue;

        public ResourceValue Value
        {
            set => _resourceValue = value;
            get => _resourceValue;
        }

        public int TotalValue => Value.TotalAmount;
    }
}
