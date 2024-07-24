using Assets.Abstractions.RPG.Items.StackableItems;
using Assets.Abstractions.RPG.Misc;
using Assets.Abstractions.RPG.Units;

namespace Assets.Abstractions.RPG.Items.UsableItems
{
    [System.Serializable]
    public abstract class UseableItem : StackableItem
    {
        public override ERuntimeItem RuntimeItemType => ERuntimeItem.Useable;
        public abstract EUseable UseableType { get; }

        public UseableItem(int amount, float data) : base()
        {
            Amount = new Value.ResourceValue();
            Amount.SetAmount(amount);
        }

        public virtual void Use(ICharacter character) 
        {
            Amount.Substract(1);
            if (Amount.TotalAmount <= 0)
            {
                Drop();
            }
        }

        public override bool IsSame(BaseRuntimeItem other)
        {
            return base.IsSame(other) && (other as UseableItem).UseableType == UseableType;
        }
    }
}
