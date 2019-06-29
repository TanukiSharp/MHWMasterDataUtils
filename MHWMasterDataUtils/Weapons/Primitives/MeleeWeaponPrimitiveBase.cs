using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

// Information based on this documentation:
// https://gitlab.com/frederik-schumacher/mhw-equipment-docs/wikis/WP_DAT

namespace MHWMasterDataUtils.Weapons.Primitives
{
    public class MeleeWeaponPrimitiveBase : WeaponPrimitiveBase
    {
        public byte kire_id;
        public byte handicraft;
        public ushort wep1_id;
        public ushort wep2_id;

        public static MeleeWeaponPrimitiveBase Read(Reader reader)
        {
            uint id = reader.ReadUInt32();
            reader.Offset(2); // Skip unknown1 and unknown2.
            ushort base_model_id = reader.ReadUInt16();
            ushort part1_id = reader.ReadUInt16();
            ushort part2_id = reader.ReadUInt16();
            byte color = reader.ReadByte();
            byte tree_id = reader.ReadByte();
            var is_fixed_upgrade = (FixedUpgradePrimitive)reader.ReadByte();
            uint crafting_cost = reader.ReadUInt32();
            byte rarity = reader.ReadByte();
            byte kire_id = reader.ReadByte();
            byte handicraft = reader.ReadByte();
            ushort raw_damage = reader.ReadUInt16();
            ushort defense = reader.ReadUInt16();
            sbyte affinity = reader.ReadSByte();
            var element_id = (WeaponElementPrimitive)reader.ReadByte();
            ushort element_damage = reader.ReadUInt16();
            var hidden_element_id = (WeaponElementPrimitive)reader.ReadByte();
            ushort hidden_element_damage = reader.ReadUInt16();
            var elderseal = (EldersealPrimitive)reader.ReadByte();
            byte num_gem_slots = reader.ReadByte();
            byte gem_slot1_lvl = reader.ReadByte();
            byte gem_slot2_lvl = reader.ReadByte();
            byte gem_slot3_lvl = reader.ReadByte();
            ushort wep1_id = reader.ReadUInt16();
            ushort wep2_id = reader.ReadUInt16();
            reader.Offset(12); // Skip unknown3, unknown4 and unknown5.
            byte tree_position = reader.ReadByte();
            ushort order = reader.ReadUInt16();
            ushort gmd_name_index = reader.ReadUInt16();
            ushort gmd_description_index = reader.ReadUInt16();
            ushort skill_id = reader.ReadUInt16();
            reader.Offset(2); // Skip unknown6.

            return new MeleeWeaponPrimitiveBase
            {
                id = id,
                base_model_id = base_model_id,
                part1_id = part1_id,
                part2_id = part2_id,
                color = color,
                tree_id = tree_id,
                is_fixed_upgrade = is_fixed_upgrade,
                crafting_cost = crafting_cost,
                rarity = rarity,
                kire_id = kire_id,
                handicraft = handicraft,
                raw_damage = raw_damage,
                defense = defense,
                affinity = affinity,
                element_id = element_id,
                element_damage = element_damage,
                hidden_element_id = hidden_element_id,
                hidden_element_damage = hidden_element_damage,
                elderseal = elderseal,
                num_gem_slots = num_gem_slots,
                gem_slot1_lvl = gem_slot1_lvl,
                gem_slot2_lvl = gem_slot2_lvl,
                gem_slot3_lvl = gem_slot3_lvl,
                wep1_id = wep1_id,
                wep2_id = wep2_id,
                tree_position = tree_position,
                order = order,
                gmd_name_index = gmd_name_index,
                gmd_description_index = gmd_description_index,
                skill_id = skill_id,

            };
        }
    }
}
