namespace Assets.Abstractions.RPG.Items.StackableItems
{
    public class StackableItem : BaseRuntimeItem
    {
        private int _amount;

        public int Amount
        {
            set => _amount = value;
            get => _amount;
        }
    }
}
