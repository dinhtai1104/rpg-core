using Assets.Abstractions.RPG.Items.StackableItems;

namespace Assets.Abstractions.RPG.UserInterface.Items
{
    public class UICurrencyItem : UIBaseStackableItem
    {
        protected new CurrencyItem RuntimeItem => (CurrencyItem)base.RuntimeItem;
    }
}
