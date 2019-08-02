using System;
using System.Collections.Generic;
using System.Text;
using MHWMasterDataUtils.Core;

namespace MHWMasterDataUtils.Equipment
{
    public class EquipmentUpgradeEntryPrimitive
    {
        public readonly byte EquipType;
        public readonly ushort EquipId;
        public readonly ushort KeyItemId;
        public readonly ushort Item1Id; // see ITM
        public readonly byte Item1Quantity;
        public readonly ushort Item2Id; // see ITM
        public readonly byte Item2Quantity;
        public readonly ushort Item3Id; // see ITM
        public readonly byte Item3Quantity;
        public readonly ushort Item4Id; // see ITM
        public readonly byte Item4Quantity;
        public readonly ushort Descendant1Id;
        public readonly ushort Descendant2Id;
        public readonly ushort Descendant3Id;
        public readonly ushort Descendant4Id;
        public readonly byte Group;

        private EquipmentUpgradeEntryPrimitive(
            byte equipType,
            ushort equipId,
            ushort keyItemId,
            ushort item1Id,
            byte item1Quantity,
            ushort item2Id,
            byte item2Quantity,
            ushort item3Id,
            byte item3Quantity,
            ushort item4Id,
            byte item4Quantity,
            ushort descendant1Id,
            ushort descendant2Id,
            ushort descendant3Id,
            ushort descendant4Id,
            byte group
        )
        {
            EquipType = equipType;
            EquipId = equipId;
            KeyItemId = keyItemId;
            Item1Id = item1Id;
            Item1Quantity = item1Quantity;
            Item2Id = item2Id;
            Item2Quantity = item2Quantity;
            Item3Id = item3Id;
            Item3Quantity = item3Quantity;
            Item4Id = item4Id;
            Item4Quantity = item4Quantity;
            Descendant1Id = descendant1Id;
            Descendant2Id = descendant2Id;
            Descendant3Id = descendant3Id;
            Descendant4Id = descendant4Id;
            Group = group;
        }

        public static EquipmentUpgradeEntryPrimitive Read(Reader reader)
        {
            byte equipType = reader.ReadByte();
            ushort equipId = reader.ReadUInt16();
            ushort keyItemId = reader.ReadUInt16();
            reader.Offset(12); // Skip unk1, unk2 and unk3.
            ushort item1Id = reader.ReadUInt16();
            byte item1Quantity = reader.ReadByte();
            ushort item2Id = reader.ReadUInt16();
            byte item2Quantity = reader.ReadByte();
            ushort item3Id = reader.ReadUInt16();
            byte item3Quantity = reader.ReadByte();
            ushort item4Id = reader.ReadUInt16();
            byte item4Quantity = reader.ReadByte();
            ushort descendant1Id = reader.ReadUInt16();
            ushort descendant2Id = reader.ReadUInt16();
            ushort descendant3Id = reader.ReadUInt16();
            ushort descendant4Id = reader.ReadUInt16();
            reader.Offset(1); // Skip unk4.
            byte group = reader.ReadByte();
            reader.Offset(2); // Skip unk5.

            return new EquipmentUpgradeEntryPrimitive(
                equipType,
                equipId,
                keyItemId,
                item1Id,
                item1Quantity,
                item2Id,
                item2Quantity,
                item3Id,
                item3Quantity,
                item4Id,
                item4Quantity,
                descendant1Id,
                descendant2Id,
                descendant3Id,
                descendant4Id,
                group
            );
        }
    }
}
