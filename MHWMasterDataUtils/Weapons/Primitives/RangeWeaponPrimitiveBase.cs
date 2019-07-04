using System;
using System.Collections.Generic;
using System.Text;
using MHWMasterDataUtils.Equipments;

namespace MHWMasterDataUtils.Weapons.Primitives
{
    public class RangeWeaponPrimitiveBase : WeaponPrimitiveBase
    {
        public MuzzelTypePrimitive MuzzelType { get; } // 0: none, 1: silencer
        public BarrelTypePrimitive BarrelType { get; } // 0: short/none, 1: rifle/long barrel
        public MagazineTypePrimitive MagazineType { get; } // 0: none, 1: extended, 2: drum
        public ScopeTypePrimitive ScopeType { get; } // 0: none, 1: scope
        public ushort ShellTableId { get; }
        public DeviationPrimitive Deviation { get; } // applies to LBG, HBG. 0: none, 1: low, 2: average, 3: high
        public sbyte SpecialAmmoId { get; } // LBG and HBG: 0: Wyvernblast, 1: Wyvernfire, 2: Wyvernsnipe. Bow: reference to common/equip/bottle_table.bbtbl

        public RangeWeaponPrimitiveBase(
            WeaponClass weaponClass,
            uint id,
            ushort baseModelId,
            ushort part1Id,
            ushort part2Id,
            byte color,
            byte treeId,
            FixedUpgradePrimitive isFixedUpgrade,
            MuzzelTypePrimitive muzzelType,
            BarrelTypePrimitive barrelType,
            MagazineTypePrimitive magazineType,
            ScopeTypePrimitive scopeType,
            uint craftingCost,
            byte rarity,
            ushort rawDamage,
            ushort defense,
            sbyte affinity,
            ElementStatus elementId,
            ushort elementDamage,
            ElementStatus hiddenElementId,
            ushort hiddenElementDamage,
            EldersealPrimitive elderseal,
            ushort shellTableId,
            DeviationPrimitive deviation,
            byte gemSlots,
            byte gemSlot1,
            byte gemSlot2,
            byte gemSlot3,
            sbyte specialAmmoId,
            byte treePosition,
            ushort order,
            ushort gmdNameIndex,
            ushort gmdDescriptionIndex,
            ushort skillId
        )
            : base(
                weaponClass,
                id,
                baseModelId,
                part1Id,
                part2Id,
                color,
                treeId,
                isFixedUpgrade,
                craftingCost,
                rarity,
                rawDamage,
                defense,
                affinity,
                elementId,
                elementDamage,
                hiddenElementId,
                hiddenElementDamage,
                elderseal,
                gemSlots,
                gemSlot1,
                gemSlot2,
                gemSlot3,
                treePosition,
                order,
                gmdNameIndex,
                gmdDescriptionIndex,
                skillId
            )
        {
            MuzzelType = muzzelType;
            BarrelType = barrelType;
            MagazineType = magazineType;
            ScopeType = scopeType;
            ShellTableId = shellTableId;
            Deviation = deviation;
            SpecialAmmoId = specialAmmoId;
        }

        public static RangeWeaponPrimitiveBase Read(WeaponClass weaponClass, Reader reader)
        {
            uint id = reader.ReadUInt32();
            reader.Offset(2); // Skip unknown1 and unknown2.
            ushort baseModelId = reader.ReadUInt16();
            ushort part1Id = reader.ReadUInt16();
            ushort part2Id = reader.ReadUInt16();
            byte color = reader.ReadByte();
            byte treeId = reader.ReadByte();
            var isFixedUpgrade = (FixedUpgradePrimitive)reader.ReadByte();
            var muzzeltype = (MuzzelTypePrimitive)reader.ReadByte();
            var barreltype = (BarrelTypePrimitive)reader.ReadByte();
            var magazineType = (MagazineTypePrimitive)reader.ReadByte();
            var scopeType = (ScopeTypePrimitive)reader.ReadByte();
            uint craftingCost = reader.ReadUInt32();
            byte rarity = reader.ReadByte();
            ushort rawDamage = reader.ReadUInt16();
            ushort defense = reader.ReadUInt16();
            sbyte affinity = reader.ReadSByte();
            var elementId = (ElementStatus)reader.ReadByte();
            ushort elementDamage = reader.ReadUInt16();
            var hiddenElementId = (ElementStatus)reader.ReadByte();
            ushort hiddenElementDamage = reader.ReadUInt16();
            var elderseal = (EldersealPrimitive)reader.ReadByte();
            ushort shellTableId = reader.ReadUInt16();
            var deviation = (DeviationPrimitive)reader.ReadByte();
            byte gemSlots = reader.ReadByte();
            byte gemSlot1 = reader.ReadByte();
            byte gemSlot2 = reader.ReadByte();
            byte gemSlot3 = reader.ReadByte();
            reader.Offset(13); // Skip unknown31 to unknown43.
            sbyte specialAmmoId = reader.ReadSByte();
            byte treePosition = reader.ReadByte();
            ushort order = reader.ReadUInt16();
            ushort gmdNameIndex = reader.ReadUInt16();
            ushort gmdDescriptionIndex = reader.ReadUInt16();
            ushort skillId = reader.ReadUInt16();
            reader.Offset(2); // Skip unknown51 and unknown52.

            return new RangeWeaponPrimitiveBase(
                weaponClass,
                id,
                baseModelId,
                part1Id,
                part2Id,
                color,
                treeId,
                isFixedUpgrade,
                muzzeltype,
                barreltype,
                magazineType,
                scopeType,
                craftingCost,
                rarity,
                rawDamage,
                defense,
                affinity,
                elementId,
                elementDamage,
                hiddenElementId,
                hiddenElementDamage,
                elderseal,
                shellTableId,
                deviation,
                gemSlots,
                gemSlot1,
                gemSlot2,
                gemSlot3,
                specialAmmoId,
                treePosition,
                order,
                gmdNameIndex,
                gmdDescriptionIndex,
                skillId
            );
        }

        public override int CompareTo(WeaponPrimitiveBase other)
        {
            int diff = base.CompareTo(other);

            var otherRangeWeapon = other as RangeWeaponPrimitiveBase;
            if (otherRangeWeapon == null)
                return diff;

            if (MuzzelType != otherRangeWeapon.MuzzelType)
                diff++;
            if (BarrelType != otherRangeWeapon.BarrelType)
                diff++;
            if (MagazineType != otherRangeWeapon.MagazineType)
                diff++;
            if (ScopeType != otherRangeWeapon.ScopeType)
                diff++;
            if (ShellTableId != otherRangeWeapon.ShellTableId)
                diff++;
            if (Deviation != otherRangeWeapon.Deviation)
                diff++;
            if (SpecialAmmoId != otherRangeWeapon.SpecialAmmoId)
                diff++;

            return diff;
        }
    }
}
