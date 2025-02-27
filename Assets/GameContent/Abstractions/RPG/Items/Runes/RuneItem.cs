using Assets.Abstractions.RPG.Items.StackableItems;
using Assets.Abstractions.RPG.Misc;

namespace Assets.Abstractions.RPG.Items.Runes
{
    [System.Serializable]
    public class RuneItem : StackableItem
    {
        private ERune _runeType;
        public ERune Rune => _runeType;
    }
}
