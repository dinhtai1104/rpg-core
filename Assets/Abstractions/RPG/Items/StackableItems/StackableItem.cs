using Assets.Abstractions.RPG.Value;

namespace Assets.Abstractions.RPG.Items.StackableItems
{
    public class StackableItem : BaseRuntimeItem
    {
        private ResourceValue _amount;
        public ResourceValue Amount
        {
            get => _amount;
            set => _amount = value;
        }
    }
}
