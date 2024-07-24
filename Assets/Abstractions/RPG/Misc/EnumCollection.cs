using Assets.Abstractions.RPG.Units.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Abstractions.RPG.Misc
{
    public static class EnumCollection
    {
        private static List<SlotType> _slotTypes;
        public static List<SlotType> SlotTypes
        {
            get
            {
                if (_slotTypes == null)
                {
                    _slotTypes = ((SlotType[])Enum.GetValues(typeof(SlotType))).ToList();
                }
                return _slotTypes;
            }
        }
    }
}
