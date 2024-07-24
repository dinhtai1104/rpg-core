using Assets.Abstractions.RPG.Items.StackableItems;
using Assets.Abstractions.RPG.Units;

namespace Assets.Abstractions.RPG.Items.UsableItems
{
    [System.Serializable]
    public class UseableItem : StackableItem
    {
        public virtual void Use(ICharacter character) 
        {
            Amount.Substract(1);
            if (Amount.TotalAmount <= 0)
            {
                Drop();
            }
        }
    }
}
