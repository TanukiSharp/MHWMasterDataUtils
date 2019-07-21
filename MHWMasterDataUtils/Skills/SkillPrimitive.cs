using System;
using System.Collections.Generic;
using System.Text;

namespace MHWMasterDataUtils.Skills
{
    public struct SkillPrimitive
    {
        public readonly byte IsSetBonus;
        public readonly byte Unknown;

        private SkillPrimitive(byte isSetBonnus, byte unknown)
        {
            IsSetBonus = isSetBonnus;
            Unknown = unknown;
        }

        public static SkillPrimitive Read(Reader reader)
        {
            byte isSetBonus = reader.ReadByte();
            byte unknown = reader.ReadByte();

            return new SkillPrimitive(isSetBonus, unknown);
        }
    }
}
