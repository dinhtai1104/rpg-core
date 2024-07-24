namespace Assets.Abstractions.RPG.Items.Accessories
{
    [System.Serializable]
    public abstract class DecoratorAccessoryItem : BaseAccessoryItem
    {
        private BaseAccessoryItem accessoryItem;

        protected DecoratorAccessoryItem(BaseAccessoryItem accessoryItem) : base()
        {
            this.accessoryItem = accessoryItem;
        }
    }
}
