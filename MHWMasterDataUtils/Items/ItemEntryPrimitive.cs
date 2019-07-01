using System;
using System.Collections.Generic;
using System.Text;

namespace MHWMasterDataUtils.Items
{
    public struct ItemEntryPrimitive
    {
        public uint id;
        public ItemSubTypePrimitive sub_type; // 0: item, 1: ammunition, 4: coating
        public ItemTypePrimitive type; // 0: Item, 1: Monster Material, 2: Endemic Life, 3: Ammunition/Coating, 4: Jewel
        public byte rarity;
        public byte carry_limit;
        public byte unk_limit;
        public ushort sort_order;
        public ItemFlagsPrimitive flags;
        public uint icon_id;
        public ushort icon_color;
        public uint sell_price;
        public uint buy_price;

        public static ItemEntryPrimitive Read(Reader reader)
        {
            uint id = reader.ReadUInt32();
            var sub_type = (ItemSubTypePrimitive)reader.ReadByte();
            var type = (ItemTypePrimitive)reader.ReadUInt32();
            byte rarity = reader.ReadByte();
            byte carry_limit = reader.ReadByte();
            byte unk_limit = reader.ReadByte();
            ushort sort_order = reader.ReadUInt16();
            var flags = (ItemFlagsPrimitive)reader.ReadUInt32();
            uint icon_id = reader.ReadUInt32();
            ushort icon_color = reader.ReadUInt16();
            uint sell_price = reader.ReadUInt32();
            uint buy_price = reader.ReadUInt32();

            return new ItemEntryPrimitive
            {
                id = id,
                sub_type = sub_type,
                type = type,
                rarity = rarity,
                carry_limit = carry_limit,
                unk_limit = unk_limit,
                sort_order = sort_order,
                flags = flags,
                icon_id = icon_id,
                icon_color = icon_color,
                sell_price = sell_price,
                buy_price = buy_price
            };
        }
    }
}
