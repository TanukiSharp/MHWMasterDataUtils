using System;
using System.Collections.Generic;
using System.Text;

namespace MHWMasterDataUtils.Weapons.Primitives
{
    public class WeaponPrimitiveBase
    {
        public WeaponType weaponType;
        public uint id;
        public byte unknown1;
        public byte unknown2;
        public ushort base_model_id;
        public ushort part1_id;
        public ushort part2_id;
        public byte color;
        public byte tree_id;
        public FixedUpgradePrimitive is_fixed_upgrade;
        public uint crafting_cost;
        public byte rarity;
        public ushort raw_damage; // raw damage, display value depends on weapon display multiplier
        public ushort defense;
        public sbyte affinity;
        public WeaponElementPrimitive element_id; // 0: none, 1: fire, 2: water, 3: ice, 4: thunder, 5: dragon, 6: poison, 7: paralysis, 8: sleep, 9: blast
        public ushort element_damage; // appears multiplied by 10 in game
        public WeaponElementPrimitive hidden_element_id; // 0: none, 1: fire, 2: water, 3: ice, 4: thunder, 5: dragon, 6: poison, 7: paralysis, 8: sleep, 9: blast
        public ushort hidden_element_damage; // appears multiplied by 10 in game
        public EldersealPrimitive elderseal; // 0: none, 1: low, 2: average, 3: high
        public byte num_gem_slots;
        public byte gem_slot1_lvl;
        public byte gem_slot2_lvl;
        public byte gem_slot3_lvl;
        public byte tree_position;
        public ushort order;
        public ushort gmd_name_index;
        public ushort gmd_description_index;
        public ushort skill_id;
    }
}
