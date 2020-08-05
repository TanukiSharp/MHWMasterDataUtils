using System;
using System.Collections.Generic;
using System.Text;
using MHWMasterDataUtils.Core;

namespace MHWMasterDataUtils.Equipment
{
    public class EquipmentCraftEntryPrimitive : IEquatable<EquipmentCraftEntryPrimitive>
    {
        public readonly byte EquipType;
        public readonly ushort EquipId;
        public readonly ushort KeyItem;
        public readonly uint Rank;
        public readonly ushort Item1Id; // see ITM
        public readonly byte Item1Quantity;
        public readonly ushort Item2Id; // see ITM
        public readonly byte Item2Quantity;
        public readonly ushort Item3Id; // see ITM
        public readonly byte Item3Quantity;
        public readonly ushort Item4Id; // see ITM
        public readonly byte Item4Quantity;

        private EquipmentCraftEntryPrimitive(
            byte equipType,
            ushort equipId,
            ushort keyItem,
            uint rank,
            ushort item1Id,
            byte item1Quantity,
            ushort item2Id,
            byte item2Quantity,
            ushort item3Id,
            byte item3Quantity,
            ushort item4Id,
            byte item4Quantity
        )
        {
            EquipType = equipType;
            EquipId = equipId;
            KeyItem = keyItem;
            Rank = rank;
            Item1Id = item1Id;
            Item1Quantity = item1Quantity;
            Item2Id = item2Id;
            Item2Quantity = item2Quantity;
            Item3Id = item3Id;
            Item3Quantity = item3Quantity;
            Item4Id = item4Id;
            Item4Quantity = item4Quantity;
        }

        public static EquipmentCraftEntryPrimitive Read(Reader reader)
        {
            byte equipType = reader.ReadByte();
            ushort equipId = reader.ReadUInt16();
            ushort keyItem = reader.ReadUInt16();
            reader.Offset(8); // Skipp unknown1 and unknown2.
            uint rank = reader.ReadUInt32();
            reader.Offset(4); // Skipp unknown3.
            ushort item1Id = reader.ReadUInt16();
            byte item1Quantity = reader.ReadByte();
            ushort item2Id = reader.ReadUInt16();
            byte item2Quantity = reader.ReadByte();
            ushort item3Id = reader.ReadUInt16();
            byte item3Quantity = reader.ReadByte();
            ushort item4Id = reader.ReadUInt16();
            byte item4Quantity = reader.ReadByte();
            reader.Offset(4); // Skip unknown4, unknown5 and unknown6, unknown7.

            return new EquipmentCraftEntryPrimitive(
                equipType,
                equipId,
                keyItem,
                rank,
                item1Id,
                item1Quantity,
                item2Id,
                item2Quantity,
                item3Id,
                item3Quantity,
                item4Id,
                item4Quantity
            );
        }

        public bool Equals(EquipmentCraftEntryPrimitive other)
        {
            if (other.EquipType != EquipType)
                return false;
            if (other.EquipId != EquipId)
                return false;
            if (other.KeyItem != KeyItem)
                return false;
            if (other.Rank != Rank)
                return false;
            if (other.Item1Id != Item1Id)
                return false;
            if (other.Item1Quantity != Item1Quantity)
                return false;
            if (other.Item2Id != Item2Id)
                return false;
            if (other.Item2Quantity != Item2Quantity)
                return false;
            if (other.Item3Id != Item3Id)
                return false;
            if (other.Item3Quantity != Item3Quantity)
                return false;
            if (other.Item4Id != Item4Id)
                return false;
            if (other.Item4Quantity != Item4Quantity)
                return false;

            return true;
        }

        public override string ToString()
        {
            return $"[{KeyItem}] type: {(WeaponType)EquipType} / {(EquipmentType)EquipType}, equipId: {EquipId}";
        }
    }
}
