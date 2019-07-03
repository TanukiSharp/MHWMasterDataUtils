using System;
using System.Collections.Generic;
using System.Text;

namespace MHWMasterDataUtils.Crafting
{
    public class CraftEntryPrimitive : IEquatable<CraftEntryPrimitive>
    {
        public byte equip_type;
        public ushort equip_id;
        public ushort key_item;
        public uint rank;
        public ushort item1_id; // see ITM
        public byte item1_qty;
        public ushort item2_id; // see ITM
        public byte item2_qty;
        public ushort item3_id; // see ITM
        public byte item3_qty;
        public ushort item4_id; // see ITM
        public byte item4_qty;

        public static CraftEntryPrimitive Read(Reader reader)
        {
            byte equip_type = reader.ReadByte();
            ushort equip_id = reader.ReadUInt16();
            ushort key_item = reader.ReadUInt16();
            reader.Offset(8); // Skipp unknown1 and unknown2.
            uint rank = reader.ReadUInt32();
            ushort item1_id = reader.ReadUInt16();
            byte item1_qty = reader.ReadByte();
            ushort item2_id = reader.ReadUInt16();
            byte item2_qty = reader.ReadByte();
            ushort item3_id = reader.ReadUInt16();
            byte item3_qty = reader.ReadByte();
            ushort item4_id = reader.ReadUInt16();
            byte item4_qty = reader.ReadByte();
            reader.Offset(4); // Skip unknown3, unknown4, unknown5 and unknown6.

            return new CraftEntryPrimitive
            {
                equip_type = equip_type,
                equip_id = equip_id,
                key_item = key_item,
                rank = rank,
                item1_id = item1_id,
                item1_qty = item1_qty,
                item2_id = item2_id,
                item2_qty = item2_qty,
                item3_id = item3_id,
                item3_qty = item3_qty,
                item4_id = item4_id,
                item4_qty = item4_qty
            };
        }

        public bool Equals(CraftEntryPrimitive other)
        {
            if (other.equip_type != equip_type)
                return false;
            if (other.equip_id != equip_id)
                return false;
            if (other.key_item != key_item)
                return false;
            if (other.rank != rank)
                return false;
            if (other.item1_id != item1_id)
                return false;
            if (other.item1_qty != item1_qty)
                return false;
            if (other.item2_id != item2_id)
                return false;
            if (other.item2_qty != item2_qty)
                return false;
            if (other.item3_id != item3_id)
                return false;
            if (other.item3_qty != item3_qty)
                return false;
            if (other.item4_id != item4_id)
                return false;
            if (other.item4_qty != item4_qty)
                return false;

            return true;
        }
    }
}
