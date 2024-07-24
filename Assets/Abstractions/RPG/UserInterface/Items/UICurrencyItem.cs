using Assets.Abstractions.RPG.Items.StackableItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Abstractions.RPG.UserInterface.Items
{
    public class UICurrencyItem : UIBaseStackableItem
    {
        protected new CurrencyItem RuntimeItem => (CurrencyItem)base.RuntimeItem;
    }
}
