using System;
using System.Collections.Generic;
using System.Text;
using MHWMasterDataUtils.Core;

namespace MHWMasterDataUtils.Equipment
{
    public class EquipmentPrimitive : IComparable<EquipmentPrimitive>
    {
        public readonly ushort Id;
        public readonly ushort Order;
        public readonly ArmorVariantPrimitive Variant;
        public readonly ushort SetId;
        public readonly ArmorTypePrimitive Type;
        public readonly EquipmentType EquipSlot;
        public readonly ushort Defense;
        public readonly ushort ModelMainId;
        public readonly ushort ModelSecondaryId;
        public readonly byte IconColor;
        public readonly byte IconEffect;
        public readonly byte Rarity;
        public readonly uint CraftingCost;
        public readonly sbyte FireRes;
        public readonly sbyte WaterRes;
        public readonly sbyte IceRes;
        public readonly sbyte ThunderRes;
        public readonly sbyte DragonRes;
        public readonly byte GemSlots;
        public readonly byte GemSlot1;
        public readonly byte GemSlot2;
        public readonly byte GemSlot3;
        public readonly ushort SetSkill1Id;
        public readonly byte SetSkill1Level;
        public readonly ushort SetSkill2Id;
        public readonly byte SetSkill2Level;
        public readonly ushort Skill1Id;
        public readonly byte Skill1Level;
        public readonly ushort Skill2Id;
        public readonly byte Skill2Level;
        public readonly ushort Skill3Id;
        public readonly byte Skill3Level;
        public readonly Gender Gender;
        public readonly ushort SetGroup;
        public readonly ushort GmdNameIndex;
        public readonly ushort GmdDescriptionIndex;
        public readonly PermanentPrimitive IsPermanent;

        private EquipmentPrimitive(
            ushort id,
            ushort order,
            ArmorVariantPrimitive variant,
            ushort setId,
            ArmorTypePrimitive type,
            EquipmentType equipSlot,
            ushort defense,
            ushort modelMainId,
            ushort modelSecondaryId,
            byte iconColor,
            byte iconEffect,
            byte rarity,
            uint craftingCost,
            sbyte fireRes,
            sbyte waterRes,
            sbyte iceRes,
            sbyte thunderRes,
            sbyte dragonRes,
            byte gemSlots,
            byte gemSlot1,
            byte gemSlot2,
            byte gemSlot3,
            ushort setSkill1Id,
            byte setSkill1Level,
            ushort setSkill2Id,
            byte setSkill2Level,
            ushort skill1Id,
            byte skill1Level,
            ushort skill2Id,
            byte skill2Level,
            ushort skill3Id,
            byte skill3Level,
            Gender gender,
            ushort setGroup,
            ushort gmdNameIndex,
            ushort gmdDescriptionIndex,
            PermanentPrimitive isPermanent
        )
        {
            Id = id;
            Order = order;
            Variant = variant;
            SetId = setId;
            Type = type;
            EquipSlot = equipSlot;
            Defense = defense;
            ModelMainId = modelMainId;
            ModelSecondaryId = modelSecondaryId;
            IconColor = iconColor;
            IconEffect = iconEffect;
            Rarity = rarity;
            CraftingCost = craftingCost;
            FireRes = fireRes;
            WaterRes = waterRes;
            IceRes = iceRes;
            ThunderRes = thunderRes;
            DragonRes = dragonRes;
            GemSlots = gemSlots;
            GemSlot1 = gemSlot1;
            GemSlot2 = gemSlot2;
            GemSlot3 = gemSlot3;
            SetSkill1Id = setSkill1Id;
            SetSkill1Level = setSkill1Level;
            SetSkill2Id = setSkill2Id;
            SetSkill2Level = setSkill2Level;
            Skill1Id = skill1Id;
            Skill1Level = skill1Level;
            Skill2Id = skill2Id;
            Skill2Level = skill2Level;
            Skill3Id = skill3Id;
            Skill3Level = skill3Level;
            Gender = gender;
            SetGroup = setGroup;
            GmdNameIndex = gmdNameIndex;
            GmdDescriptionIndex = gmdDescriptionIndex;
            IsPermanent = isPermanent;
        }

        public static EquipmentPrimitive Read(Reader reader)
        {
            ushort id = reader.ReadUInt16();
            reader.Offset(2); // Skip unk1 and unk2.
            ushort order = reader.ReadUInt16();
            var variant = (ArmorVariantPrimitive)reader.ReadByte();
            ushort setId = reader.ReadUInt16();
            var type = (ArmorTypePrimitive)reader.ReadByte();
            var equipSlot = (EquipmentType)reader.ReadByte();
            ushort defense = reader.ReadUInt16();
            ushort modelMainId = reader.ReadUInt16();
            ushort modelSecondaryId = reader.ReadUInt16();
            byte iconColor = reader.ReadByte();
            reader.Offset(1); // Skip unk3.
            byte iconEffect = reader.ReadByte();
            byte rarity = (byte)(reader.ReadByte() + 1);
            uint craftingCost = reader.ReadUInt32();
            sbyte fireRes = reader.ReadSByte();
            sbyte waterRes = reader.ReadSByte();
            sbyte iceRes = reader.ReadSByte();
            sbyte thunderRes = reader.ReadSByte();
            sbyte dragonRes = reader.ReadSByte();
            byte gemSlots = reader.ReadByte();
            byte gemSlot1 = reader.ReadByte();
            byte gemSlot2 = reader.ReadByte();
            byte gemSlot3 = reader.ReadByte();
            ushort setSkill1Id = reader.ReadUInt16();
            byte setSkill1Level = reader.ReadByte();
            ushort setSkill2Id = reader.ReadUInt16();
            byte setSkill2Level = reader.ReadByte();
            ushort skill1Id = reader.ReadUInt16();
            byte skill1Level = reader.ReadByte();
            ushort skill2Id = reader.ReadUInt16();
            byte skill2Level = reader.ReadByte();
            ushort skill3Id = reader.ReadUInt16();
            byte skill3Level = reader.ReadByte();
            var gender = (Gender)reader.ReadByte();
            reader.Offset(3); // Skip unk4, unk5 and unk6.
            ushort setGroup = reader.ReadUInt16();
            ushort gmdNameIndex = reader.ReadUInt16();
            ushort gmdDescriptionIndex = reader.ReadUInt16();
            var isPermanent = (PermanentPrimitive)reader.ReadByte();

            return new EquipmentPrimitive(
                id,
                order,
                variant,
                setId,
                type,
                equipSlot,
                defense,
                modelMainId,
                modelSecondaryId,
                iconColor,
                iconEffect,
                rarity,
                craftingCost,
                fireRes,
                waterRes,
                iceRes,
                thunderRes,
                dragonRes,
                gemSlots,
                gemSlot1,
                gemSlot2,
                gemSlot3,
                setSkill1Id,
                setSkill1Level,
                setSkill2Id,
                setSkill2Level,
                skill1Id,
                skill1Level,
                skill2Id,
                skill2Level,
                skill3Id,
                skill3Level,
                gender,
                setGroup,
                gmdNameIndex,
                gmdDescriptionIndex,
                isPermanent
            );
        }

        public int CompareTo(EquipmentPrimitive other)
        {
            int diff = 0;

            if (Id != other.Id)
                diff++;
            if (Order != other.Order)
                diff++;
            if (Variant != other.Variant)
                diff++;
            if (SetId != other.SetId)
                diff++;
            if (Type != other.Type)
                diff++;
            if (EquipSlot != other.EquipSlot)
                diff++;
            if (Defense != other.Defense)
                diff++;
            if (ModelMainId != other.ModelMainId)
                diff++;
            if (ModelSecondaryId != other.ModelSecondaryId)
                diff++;
            if (IconColor != other.IconColor)
                diff++;
            if (IconEffect != other.IconEffect)
                diff++;
            if (Rarity != other.Rarity)
                diff++;
            if (CraftingCost != other.CraftingCost)
                diff++;
            if (FireRes != other.FireRes)
                diff++;
            if (WaterRes != other.WaterRes)
                diff++;
            if (IceRes != other.IceRes)
                diff++;
            if (ThunderRes != other.ThunderRes)
                diff++;
            if (DragonRes != other.DragonRes)
                diff++;
            if (GemSlots != other.GemSlots)
                diff++;
            if (GemSlot1 != other.GemSlot1)
                diff++;
            if (GemSlot2 != other.GemSlot2)
                diff++;
            if (GemSlot3 != other.GemSlot3)
                diff++;
            if (SetSkill1Id != other.SetSkill1Id)
                diff++;
            if (SetSkill1Level != other.SetSkill1Level)
                diff++;
            if (SetSkill2Id != other.SetSkill2Id)
                diff++;
            if (SetSkill2Level != other.SetSkill2Level)
                diff++;
            if (Skill1Id != other.Skill1Id)
                diff++;
            if (Skill1Level != other.Skill1Level)
                diff++;
            if (Skill2Id != other.Skill2Id)
                diff++;
            if (Skill2Level != other.Skill2Level)
                diff++;
            if (Skill3Id != other.Skill3Id)
                diff++;
            if (Skill3Level != other.Skill3Level)
                diff++;
            if (Gender != other.Gender)
                diff++;
            if (SetGroup != other.SetGroup)
                diff++;
            if (GmdNameIndex != other.GmdNameIndex)
                diff++;
            if (GmdDescriptionIndex != other.GmdDescriptionIndex)
                diff++;
            if (IsPermanent != other.IsPermanent)
                diff++;

            return diff;
        }
    }
}
