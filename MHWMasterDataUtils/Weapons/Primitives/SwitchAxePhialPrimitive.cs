using System;
using System.Collections.Generic;
using System.Text;

namespace MHWMasterDataUtils.Weapons.Primitives
{
    public class SwitchAxePhialPrimitive
    {
        public readonly ushort Id;
        public readonly byte PhialType;
        public readonly ushort Damage;

        private SwitchAxePhialPrimitive(ushort id, byte phialType, ushort damage)
        {
            Id = id;
            PhialType = phialType;
            Damage = damage;
        }

        public static SwitchAxePhialPrimitive Read(Reader reader)
        {
            ushort id = reader.ReadUInt16();
            reader.Offset(2);
            byte phialType = reader.ReadByte();
            ushort damage = reader.ReadUInt16();

            return new SwitchAxePhialPrimitive(id, phialType, damage);
        }
    }
}
