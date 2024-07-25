using Assets.Abstractions.RPG.Items.StackableItems;

namespace Assets.Abstractions.RPG.UserInterface.Items
{
    public abstract class UIBaseStackableItem : UIBaseItem<StackableItem>
    {
        protected override void SetData()
        {
            _slotItem.SetAmount(RuntimeItem.Amount.TotalAmount);
        }
    }
}
