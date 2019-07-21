using System;
using System.Collections.Generic;
using System.Text;

namespace MHWMasterDataUtils.Skills
{
    public class SkillAbilityPrimitive
    {
        public readonly uint Index;
        public readonly ushort SkillId;
        public readonly byte Level;
        public readonly ushort Param1;
        public readonly ushort Param2;
        public readonly ushort Param3;
        public readonly ushort Param4;

        private SkillAbilityPrimitive(
            uint index,
            ushort skillId,
            byte level,
            ushort param1,
            ushort param2,
            ushort param3,
            ushort param4
        )
        {
            Index = index;
            SkillId = skillId;
            Level = level;
            Param1 = param1;
            Param2 = param2;
            Param3 = param3;
            Param4 = param4;
        }

        public static SkillAbilityPrimitive Read(uint index, Reader reader)
        {
            ushort skillId = reader.ReadUInt16();
            byte level = reader.ReadByte();
            ushort param1 = reader.ReadUInt16();
            ushort param2 = reader.ReadUInt16();
            ushort param3 = reader.ReadUInt16();
            ushort param4 = reader.ReadUInt16();

            return new SkillAbilityPrimitive(
                index,
                skillId,
                level,
                param1,
                param2,
                param3,
                param4
            );
        }
    }
}
