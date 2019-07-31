﻿using System;
using System.Collections.Generic;
using System.Text;
using MHWMasterDataUtils.Armors;

namespace MHWMasterDataUtils.Armors
{
    public class EquipmentUtils
    {
        public static int[] CreateSlotsArray(ArmorPrimitive equipment)
        {
            int[] slots = new int[equipment.GemSlots];

            if (slots.Length > 0)
            {
                slots[0] = equipment.GemSlot1;
                if (slots.Length > 1)
                {
                    slots[1] = equipment.GemSlot2;
                    if (slots.Length > 2)
                        slots[2] = equipment.GemSlot3;
                }
            }

            return slots;
        }

    }
}
