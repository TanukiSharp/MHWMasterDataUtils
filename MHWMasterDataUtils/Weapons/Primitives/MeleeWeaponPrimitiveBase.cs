using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MHWMasterDataUtils.Core;
using MHWMasterDataUtils.Equipments;
using MHWMasterDataUtils.Weapons.HighLevel;

namespace MHWMasterDataUtils.Weapons.Primitives
{
    public class MeleeWeaponPrimitiveBase : WeaponPrimitiveBase
    {
        public readonly byte SharpnessId;
        public readonly byte Handicraft;
        public readonly ushort Weapon1Id;
        public readonly ushort Weapon2Id;

        private MeleeWeaponPrimitiveBase(
            WeaponClass weaponClass,
            uint id,
            ushort baseModelId,
            ushort part1Id,
            ushort part2Id,
            byte color,
            byte treeId,
            FixedUpgradePrimitive isFixedUpgrade,
            uint craftingCost,
            byte rarity,
            byte sharpnessId,
            byte handicraft,
            ushort rawDamage,
            ushort defense,
            sbyte affinity,
            ElementStatus elementId,
            ushort elementDamage,
            ElementStatus hiddenElementId,
            ushort hiddenElementDamage,
            Elderseal elderseal,
            byte gemSlots,
            byte gemSlot1,
            byte gemSlot2,
            byte gemSlot3,
            ushort weapon1Id,
            ushort weapon2Id,
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
            SharpnessId = sharpnessId;
            Handicraft = handicraft;
            Weapon1Id = weapon1Id;
            Weapon2Id = weapon2Id;
        }

        public static MeleeWeaponPrimitiveBase Read(WeaponClass weaponClass, Reader reader)
        {
            uint id = reader.ReadUInt32();
            reader.Offset(2); // Skip unknown1 and unknown2.
            ushort baseModelId = reader.ReadUInt16();
            ushort part1Id = reader.ReadUInt16();
            ushort part2Id = reader.ReadUInt16();
            byte color = reader.ReadByte();
            byte treeId = reader.ReadByte();
            var isFixedUpgrade = (FixedUpgradePrimitive)reader.ReadByte();
            uint craftingCost = reader.ReadUInt32();
            byte rarity = reader.ReadByte();
            byte sharpnessId = reader.ReadByte();
            byte handicraft = reader.ReadByte();
            ushort rawDamage = reader.ReadUInt16();
            ushort defense = reader.ReadUInt16();
            sbyte affinity = reader.ReadSByte();
            var elementId = (ElementStatus)reader.ReadByte();
            ushort elementDamage = reader.ReadUInt16();
            var hiddenElementId = (ElementStatus)reader.ReadByte();
            ushort hiddenElementDamage = reader.ReadUInt16();
            var elderseal = (Elderseal)reader.ReadByte();
            byte gemSlots = reader.ReadByte();
            byte gemSlot1 = reader.ReadByte();
            byte gemSlot2 = reader.ReadByte();
            byte gemSlot3 = reader.ReadByte();
            ushort weapon1Id = reader.ReadUInt16();
            ushort weapon2Id = reader.ReadUInt16();
            reader.Offset(12); // Skip unknown3, unknown4 and unknown5.
            byte treePosition = reader.ReadByte();
            ushort order = reader.ReadUInt16();
            ushort gmdNameIndex = reader.ReadUInt16();
            ushort gmdDescriptionIndex = reader.ReadUInt16();
            ushort skillId = reader.ReadUInt16();
            reader.Offset(2); // Skip unknown6.

            return new MeleeWeaponPrimitiveBase(
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
                sharpnessId,
                handicraft,
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
                weapon1Id,
                weapon2Id,
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

            var otherMeleeWeapon = other as MeleeWeaponPrimitiveBase;
            if (otherMeleeWeapon == null)
                return diff;

            if (SharpnessId != otherMeleeWeapon.SharpnessId)
                diff++;
            if (Handicraft != otherMeleeWeapon.Handicraft)
                diff++;
            if (Weapon1Id != otherMeleeWeapon.Weapon1Id)
                diff++;
            if (Weapon2Id != otherMeleeWeapon.Weapon2Id)
                diff++;

            return diff;
        }
    }
}
