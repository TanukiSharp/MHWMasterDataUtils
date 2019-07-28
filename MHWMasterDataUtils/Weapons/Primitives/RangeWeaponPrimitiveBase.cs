using System;
using System.Collections.Generic;
using System.Text;
using MHWMasterDataUtils.Core;

namespace MHWMasterDataUtils.Weapons.Primitives
{
    public class RangeWeaponPrimitiveBase : WeaponPrimitiveBase
    {
        public readonly MuzzelTypePrimitive MuzzelType; // 0: none, 1: silencer
        public readonly BarrelTypePrimitive BarrelType; // 0: short/none, 1: rifle/long barrel
        public readonly MagazineTypePrimitive MagazineType; // 0: none, 1: extended, 2: drum
        public readonly ScopeTypePrimitive ScopeType; // 0: none, 1: scope
        public readonly ushort ShellTableId;
        public readonly BowgunDeviation Deviation; // applies to LBG, HBG. 0: none, 1: low, 2: average, 3: high
        public readonly byte SpecialAmmoId; // LBG and HBG: 0: Wyvernblast, 1: Wyvernfire, 2: Wyvernsnipe. Bow: reference to common/equip/bottle_table.bbtbl

        private RangeWeaponPrimitiveBase(
            WeaponType weaponType,
            uint id,
            ushort treeOrder,
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
            Elderseal elderseal,
            ushort shellTableId,
            BowgunDeviation deviation,
            byte gemSlots,
            byte gemSlot1,
            byte gemSlot2,
            byte gemSlot3,
            byte specialAmmoId,
            byte treePosition,
            ushort order,
            ushort gmdNameIndex,
            ushort gmdDescriptionIndex,
            ushort skillId
        )
            : base(
                weaponType,
                id,
                treeOrder,
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

        public static RangeWeaponPrimitiveBase Read(WeaponType weaponType, Reader reader)
        {
            uint id = reader.ReadUInt32();
            ushort treeOrder = reader.ReadUInt16();
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
            var elderseal = (Elderseal)reader.ReadByte();
            ushort shellTableId = reader.ReadUInt16();
            var deviation = (BowgunDeviation)reader.ReadByte();
            byte gemSlots = reader.ReadByte();
            byte gemSlot1 = reader.ReadByte();
            byte gemSlot2 = reader.ReadByte();
            byte gemSlot3 = reader.ReadByte();
            reader.Offset(13); // Skip unknown31 to unknown43.
            byte specialAmmoId = reader.ReadByte();
            byte treePosition = reader.ReadByte();
            ushort order = reader.ReadUInt16();
            ushort gmdNameIndex = reader.ReadUInt16();
            ushort gmdDescriptionIndex = reader.ReadUInt16();
            ushort skillId = reader.ReadUInt16();
            reader.Offset(2); // Skip unknown51 and unknown52.

            return new RangeWeaponPrimitiveBase(
                weaponType,
                id,
                treeOrder,
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
