using Assets.Abstractions.RPG.Misc;
using Assets.Abstractions.RPG.Value;

namespace Assets.Abstractions.RPG.Items.StackableItems
{
    [System.Serializable]
    public class StackableItem : BaseRuntimeItem
    {
        public override ERuntimeItem RuntimeItemType => ERuntimeItem.None;

        private ResourceValue _amount;
        public ResourceValue Amount
        {
            get => _amount;
            set => _amount = value;
        }

        public override void Add(BaseRuntimeItem other)
        {
            base.Add(other);
            Amount.Add(1);
        }
    }
}
