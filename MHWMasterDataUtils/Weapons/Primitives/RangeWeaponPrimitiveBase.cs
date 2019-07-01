using System;
using System.Collections.Generic;
using System.Text;
using MHWMasterDataUtils.Equipments;

namespace MHWMasterDataUtils.Weapons.Primitives
{
    public class RangeWeaponPrimitiveBase : WeaponPrimitiveBase
    {
        public MuzzelTypePrimitive muzzel_type; // 0: none, 1: silencer
        public BarrelTypePrimitive barrel_type; // 0: short/none, 1: rifle/long barrel
        public MagazineTypePrimitive magazine_type; // 0: none, 1: extended, 2: drum
        public ScopeTypePrimitive scope_type; // 0: none, 1: scope
        public ushort shell_table_id;
        public DeviationPrimitive deviation; // applies to LBG, HBG. 0: none, 1: low, 2: average, 3: high
        public sbyte special_ammo_id; // LBG and HBG: 0: Wyvernblast, 1: Wyvernfire, 2: Wyvernsnipe. Bow: reference to common/equip/bottle_table.bbtbl

        public static RangeWeaponPrimitiveBase Read(Reader reader)
        {
            uint id = reader.ReadUInt32();
            reader.Offset(2); // Skip unknown1 and unknown2.
            ushort base_model_id = reader.ReadUInt16();
            ushort part1_id = reader.ReadUInt16();
            ushort part2_id = reader.ReadUInt16();
            byte color = reader.ReadByte();
            byte tree_id = reader.ReadByte();
            var is_fixed_upgrade = (FixedUpgradePrimitive)reader.ReadByte();
            var muzzel_type = (MuzzelTypePrimitive)reader.ReadByte();
            var barrel_type = (BarrelTypePrimitive)reader.ReadByte();
            var magazine_type = (MagazineTypePrimitive)reader.ReadByte();
            var scope_type = (ScopeTypePrimitive)reader.ReadByte();
            uint crafting_cost = reader.ReadUInt32();
            byte rarity = reader.ReadByte();
            ushort raw_damage = reader.ReadUInt16();
            ushort defense = reader.ReadUInt16();
            sbyte affinity = reader.ReadSByte();
            var element_id = (ElementStatus)reader.ReadByte();
            ushort element_damage = reader.ReadUInt16();
            var hidden_element_id = (ElementStatus)reader.ReadByte();
            ushort hidden_element_damage = reader.ReadUInt16();
            var elderseal = (EldersealPrimitive)reader.ReadByte();
            ushort shell_table_id = reader.ReadUInt16();
            var deviation = (DeviationPrimitive)reader.ReadByte();
            byte num_gem_slots = reader.ReadByte();
            byte gem_slot1_lvl = reader.ReadByte();
            byte gem_slot2_lvl = reader.ReadByte();
            byte gem_slot3_lvl = reader.ReadByte();
            reader.Offset(13); // Skip unknown31 to unknown43.
            sbyte special_ammo_id = reader.ReadSByte();
            byte tree_position = reader.ReadByte();
            ushort order = reader.ReadUInt16();
            ushort gmd_name_index = reader.ReadUInt16();
            ushort gmd_description_index = reader.ReadUInt16();
            ushort skill_id = reader.ReadUInt16();
            reader.Offset(2); // Skip unknown51 and unknown52.

            return new RangeWeaponPrimitiveBase
            {
                id = id,
                base_model_id = base_model_id,
                part1_id = part1_id,
                part2_id = part2_id,
                color = color,
                tree_id = tree_id,
                is_fixed_upgrade = is_fixed_upgrade,
                muzzel_type = muzzel_type,
                barrel_type = barrel_type,
                magazine_type = magazine_type,
                scope_type = scope_type,
                crafting_cost = crafting_cost,
                rarity = rarity,
                raw_damage = raw_damage,
                defense = defense,
                affinity = affinity,
                element_id = element_id,
                element_damage = element_damage,
                hidden_element_id = hidden_element_id,
                hidden_element_damage = hidden_element_damage,
                elderseal = elderseal,
                shell_table_id = shell_table_id,
                deviation = deviation,
                num_gem_slots = num_gem_slots,
                gem_slot1_lvl = gem_slot1_lvl,
                gem_slot2_lvl = gem_slot2_lvl,
                gem_slot3_lvl = gem_slot3_lvl,
                special_ammo_id = special_ammo_id,
                tree_position = tree_position,
                order = order,
                gmd_name_index = gmd_name_index,
                gmd_description_index = gmd_description_index,
                skill_id = skill_id
            };
        }

        public override bool Compare(WeaponPrimitiveBase other)
        {
            if (base.Compare(other) == false)
                return false;

            var m = other as RangeWeaponPrimitiveBase;
            if (m == null)
                return false;

            if (m.muzzel_type != muzzel_type)
                return false;
            if (m.barrel_type != barrel_type)
                return false;
            if (m.magazine_type != magazine_type)
                return false;
            if (m.scope_type != scope_type)
                return false;
            if (m.shell_table_id != shell_table_id)
                return false;
            if (m.deviation != deviation)
                return false;
            if (m.special_ammo_id != special_ammo_id)
                return false;

            return true;
        }
    }
}
