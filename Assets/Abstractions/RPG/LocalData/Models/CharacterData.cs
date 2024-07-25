using Assets.Abstractions.RPG.Attributes;
using Assets.Abstractions.RPG.Inventory;
using Assets.Abstractions.RPG.Units;

namespace Assets.Abstractions.RPG.LocalData.Models
{
    [System.Serializable]
    public class CharacterData
    {
        public CharacterType Character;
        public IAttributeGroup Attributes;
        public InventoryHandler Inventory;
    }
}
