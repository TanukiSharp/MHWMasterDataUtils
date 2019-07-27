using System;
using System.Collections.Generic;
using System.Text;

namespace MHWMasterDataUtils.Weapons.Primitives
{
    public class AxePhialPrimitive
    {
        public readonly ushort Id;
        public readonly byte PhialType;
        public readonly ushort Damage;

        private AxePhialPrimitive(ushort id, byte phialType, ushort damage)
        {
            Id = id;
            PhialType = phialType;
            Damage = damage;
        }

        public static AxePhialPrimitive Read(Reader reader)
        {
            ushort id = reader.ReadUInt16();
            reader.Offset(2);
            byte phialType = reader.ReadByte();
            ushort damage = reader.ReadUInt16();

            return new AxePhialPrimitive(id, phialType, damage);
        }
    }
}
