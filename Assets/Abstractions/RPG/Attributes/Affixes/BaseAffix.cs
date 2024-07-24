using Assets.Abstractions.RPG.Items;
using System.Collections.Generic;

namespace Assets.Abstractions.RPG.Attributes
{
    public abstract class BaseAffix
    {
        public int Id { get; }
        public abstract string AffixDescription { protected set; get; }
        public abstract IList<string> SerializedValues { get; }
        public string DescKey { get; }

        public BaseAffix(int id, string descKey)
        {
            Id = id;
            DescKey = descKey;
        }

        public virtual object Clone()
        {
            var baseAffix = (BaseAffix)this.MemberwiseClone();
            return baseAffix;
        }

        public virtual void OnAddToItem(BaseRuntimeItem item)
        {
        }

        public virtual void OnRemoveFromItem(BaseRuntimeItem item)
        {
        }

        public virtual void OnEquip(IAttributeGroup stats)
        {
        }

        public virtual void OnUnequip()
        {
        }
    }
}
