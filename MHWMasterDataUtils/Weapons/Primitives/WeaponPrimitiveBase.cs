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

        public virtual bool Compare(WeaponPrimitiveBase other)
        {
            if (other.weaponType != weaponType)
                return false;
            if (other.id != id)
                return false;
            if (other.unknown1 != unknown1)
                return false;
            if (other.unknown2 != unknown2)
                return false;
            if (other.base_model_id != base_model_id)
                return false;
            if (other.part1_id != part1_id)
                return false;
            if (other.part2_id != part2_id)
                return false;
            if (other.color != color)
                return false;
            if (other.tree_id != tree_id)
                return false;
            if (other.is_fixed_upgrade != is_fixed_upgrade)
                return false;
            if (other.crafting_cost != crafting_cost)
                return false;
            if (other.rarity != rarity)
                return false;
            if (other.raw_damage != raw_damage)
                return false;
            if (other.defense != defense)
                return false;
            if (other.affinity != affinity)
                return false;
            if (other.element_id != element_id)
                return false;
            if (other.element_damage != element_damage)
                return false;
            if (other.hidden_element_id != hidden_element_id)
                return false;
            if (other.hidden_element_damage != hidden_element_damage)
                return false;
            if (other.elderseal != elderseal)
                return false;
            if (other.num_gem_slots != num_gem_slots)
                return false;
            if (other.gem_slot1_lvl != gem_slot1_lvl)
                return false;
            if (other.gem_slot2_lvl != gem_slot2_lvl)
                return false;
            if (other.gem_slot3_lvl != gem_slot3_lvl)
                return false;
            if (other.tree_position != tree_position)
                return false;
            if (other.order != order)
                return false;
            if (other.gmd_name_index != gmd_name_index)
                return false;
            if (other.gmd_description_index != gmd_description_index)
                return false;
            if (other.skill_id != skill_id)
                return false;

            return true;
        }
    }
}
