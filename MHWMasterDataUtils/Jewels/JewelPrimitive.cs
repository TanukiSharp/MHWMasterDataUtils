using System;
using System.Collections.Generic;
using System.Text;

namespace MHWMasterDataUtils.Jewels
{
    public class JewelPrimitive
    {
        public readonly uint Id;
        public readonly uint Order;
        public readonly uint Size;
        public readonly uint Skill1Id;
        public readonly uint Skill1Level;
        public readonly uint Skill2Id;
        public readonly uint Skill2Level;

        private JewelPrimitive(
            uint id,
            uint order,
            uint size,
            uint skill1Id,
            uint skill1Level,
            uint skill2Id,
            uint skill2Level
        )
        {
            Id = id;
            Order = order;
            Size = size;
            Skill1Id = skill1Id;
            Skill1Level = skill1Level;
            Skill2Id = skill2Id;
            Skill2Level = skill2Level;
        }

        public static JewelPrimitive Read(Reader reader)
        {
            uint id = reader.ReadUInt32();
            uint order = reader.ReadUInt32();
            uint size = reader.ReadUInt32();
            uint skill1Id = reader.ReadUInt32();
            uint skill1Level = reader.ReadUInt32();
            uint skill2Id = reader.ReadUInt32();
            uint skill2Level = reader.ReadUInt32();

            return new JewelPrimitive(
                id,
                order,
                size,
                skill1Id,
                skill1Level,
                skill2Id,
                skill2Level
            );
        }
    }
}
