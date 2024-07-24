using Assets.Abstractions.RPG.Items.UsableItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Abstractions.RPG.UserInterface.Items
{
    public class UIUsableItem : UIBaseStackableItem
    {
        protected new UsableItem RuntimeItem => (UsableItem)base.RuntimeItem;
    }
}
