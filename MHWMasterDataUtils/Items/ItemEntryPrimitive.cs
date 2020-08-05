using System;
using System.Collections.Generic;
using System.Text;

namespace MHWMasterDataUtils.Items
{
    public class ItemEntryPrimitive
    {
        public readonly uint Id;
        public readonly ItemSubTypePrimitive SubType; // 0: item, 1: ammunition, 4: coating
        public readonly ItemTypePrimitive Type; // 0: Item, 1: Monster Material, 2: Endemic Life, 3: Ammunition/Coating, 4: Jewel
        public readonly byte Rarity;
        public readonly byte CarryLimit;
        public readonly byte UnknownLimit;
        public readonly ushort SortOrder;
        public readonly ItemFlagsPrimitive Flags;
        public readonly uint IconId;
        public readonly byte IconColor;
        public readonly byte CarryItem;
        public readonly uint SellPrice;
        public readonly uint BuyPrice;

        private ItemEntryPrimitive(
            uint id,
            ItemSubTypePrimitive subType,
            ItemTypePrimitive type,
            byte rarity,
            byte carryLimit,
            byte unknownLimit,
            ushort sortOrder,
            ItemFlagsPrimitive flags,
            uint iconId,
            byte iconColor,
            byte carryItem,
            uint sellPrice,
            uint buyPrice
        )
        {
            Id = id;
            SubType = subType;
            Type = type;
            Rarity = rarity;
            CarryLimit = carryLimit;
            UnknownLimit = unknownLimit;
            SortOrder = sortOrder;
            Flags = flags;
            IconId = iconId;
            IconColor = iconColor;
            CarryItem = carryItem;
            SellPrice = sellPrice;
            BuyPrice = buyPrice;
        }

        public static ItemEntryPrimitive Read(Reader reader)
        {
            uint id = reader.ReadUInt32();
            var subType = (ItemSubTypePrimitive)reader.ReadByte();
            var type = (ItemTypePrimitive)reader.ReadUInt32();
            byte rarity = reader.ReadByte();
            byte carryLimit = reader.ReadByte();
            byte unknownLimit = reader.ReadByte();
            ushort sortOrder = reader.ReadUInt16();
            var flags = (ItemFlagsPrimitive)reader.ReadUInt32();
            uint iconId = reader.ReadUInt32();
            byte iconColor = reader.ReadByte();
            byte carryItem = reader.ReadByte();
            uint sellPrice = reader.ReadUInt32();
            uint buyPrice = reader.ReadUInt32();

            return new ItemEntryPrimitive(
                id,
                subType,
                type,
                (byte)(rarity + 1),
                carryLimit,
                unknownLimit,
                sortOrder,
                flags,
                iconId,
                iconColor,
                carryItem,
                sellPrice,
                buyPrice
            );
        }
    }
}
