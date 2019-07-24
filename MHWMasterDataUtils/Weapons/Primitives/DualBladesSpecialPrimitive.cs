using System;
using System.Collections.Generic;
using System.Text;
using MHWMasterDataUtils.Core;

namespace MHWMasterDataUtils.Weapons.Primitives
{
    public class DualBladesSpecialPrimitive
    {
        public readonly ushort Id;
        public readonly ElementStatus Element1;
        public readonly ushort Element1Damage;
        public readonly ElementStatus Element2;
        public readonly ushort Element2Damage;

        private DualBladesSpecialPrimitive(
            ushort id,
            ElementStatus element1,
            ushort element1Damage,
            ElementStatus element2,
            ushort element2Damage
        )
        {
            Id = id;
            Element1 = element1;
            Element1Damage = element1Damage;
            Element2 = element2;
            Element2Damage = element2Damage;
        }

        public static DualBladesSpecialPrimitive Read(Reader reader)
        {
            ushort id = reader.ReadUInt16();
            reader.Offset(2);
            var element1 = (ElementStatus)reader.ReadByte();
            ushort element1Damage = reader.ReadUInt16();
            var element2 = (ElementStatus)reader.ReadByte();
            ushort element2Damage = reader.ReadUInt16();

            return new DualBladesSpecialPrimitive(id, element1, element1Damage, element2, element2Damage);
        }
    }
}
