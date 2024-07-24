using Assets.Abstractions.RPG.Attributes;
using Assets.Abstractions.RPG.Misc;
using Assets.Abstractions.RPG.Units.Equipment;
using System.Collections.Generic;

namespace Assets.Abstractions.RPG.Items.Accessories
{
    public delegate void OnEnhanceDelegate(int levelEnhance);
    public class BaseAccessoryItem : BaseRuntimeItem
    {
        protected SlotType _slotType;
        protected bool _isEquipped;
        protected int _level;
        protected int _maxLevel;
        protected ERarity _rarity;
        protected StatAffix _baseStatAffix;

        public SlotType Slot { get => _slotType; set => _slotType = value; }
        public bool IsEquipped { get => _isEquipped; set => _isEquipped = value; }
        public int Level { set => _level = value; get => _level; }
        public int MaxLevel { set => _maxLevel = value; get => _maxLevel; }
        public ERarity Rarity { set => _rarity = value; get => _rarity; }
        public StatAffix BaseStatAffix { set => _baseStatAffix = value; get => _baseStatAffix; }


        // Sub stats
        private List<BaseAffix> _baseAffixes;
        private List<BaseAffix> _subAffixes;
        private List<BaseAffix> _hiddenAffixes;
        private List<BaseAffix> _runeAffixes;


        public OnEnhanceDelegate OnEnhanceEvent;

        #region Main Method
        public virtual void OnEquip(IAttributeGroup attributeGroup) 
        {
            _baseStatAffix.OnEquip(attributeGroup);

            foreach (var affix in _baseAffixes)
            {
                affix.OnEquip(attributeGroup);
            }

            foreach (var affix in _hiddenAffixes)
            {
                affix.OnEquip(attributeGroup);
            }

            foreach (var affix in _subAffixes)
            {
                affix.OnEquip(attributeGroup);
            }
        }

        public virtual void OnUnequip() 
        {
            _baseStatAffix.OnUnequip();

            foreach (var affix in _baseAffixes)
            {
                affix.OnUnequip();
            }

            foreach (var affix in _hiddenAffixes)
            {
                affix.OnUnequip();
            }

            foreach (var affix in _subAffixes)
            {
                affix.OnUnequip();
            }
        }
        public virtual void Enhance()
        {
            if (Level >= MaxLevel) return;
            Level++;
            OnEnhanceEvent?.Invoke(Level);
        }
        #endregion

        #region Stats

        public void AddHiddenAffix(BaseAffix affix)
        {
            _hiddenAffixes.Add(affix);
            affix.OnAddToItem(this);
        }

        public void ClearHiddenAffixes()
        {
            foreach (var affix in _hiddenAffixes)
            {
                affix.OnRemoveFromItem(this);
            }
            _hiddenAffixes.Clear();
        }

        public void AddBaseAffix(BaseAffix affix)
        {
            _baseAffixes.Add(affix);
            affix.OnAddToItem(this);
        }

        public void ClearBaseAffixes()
        {
            foreach (var affix in _baseAffixes)
            {
                affix.OnRemoveFromItem(this);
            }
            _baseAffixes.Clear();
        }

        public void AddSubAffixes(BaseAffix affix)
        {
            _subAffixes.Add(affix);
            affix.OnAddToItem(this);
        }

        public void ClearSubAffixes()
        {
            foreach (var affix in _subAffixes)
            {
                affix.OnRemoveFromItem(this);
            }
            _subAffixes.Clear();
        }

        #endregion
    }
}
