using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Abstractions.RPG.Perks
{
    public abstract class BasePerk : IPerk
    {
        protected List<IPerkAction> perkAction;
        public virtual string GetLocalize()
        {
            return "Perk";
        }
    }
}
