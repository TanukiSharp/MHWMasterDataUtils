using System;
using System.Collections.Generic;
using System.Text;

namespace MHWMasterDataUtils.Weapons.Primitives
{
    public class AxePhialPrimitive
    {
        public readonly ushort Id;
        public readonly AxeElementStatus ElementStatus;
        public readonly ushort Damage;

        private AxePhialPrimitive(ushort id, AxeElementStatus elementStatus, ushort damage)
        {
            Id = id;
            ElementStatus = elementStatus;
            Damage = damage;
        }

        public static AxePhialPrimitive Read(Reader reader)
        {
            ushort id = reader.ReadUInt16();
            reader.Offset(2);
            var elementStatus = (AxeElementStatus)reader.ReadByte();
            ushort damage = reader.ReadUInt16();

            return new AxePhialPrimitive(id, elementStatus, damage);
        }
    }
}
