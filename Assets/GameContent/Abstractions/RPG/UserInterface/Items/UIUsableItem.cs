using Assets.Abstractions.RPG.Items.UsableItems;

namespace Assets.Abstractions.RPG.UserInterface.Items
{
    public class UIUsableItem : UIBaseStackableItem
    {
        protected new UsableItem RuntimeItem => (UsableItem)base.RuntimeItem;
    }
}
