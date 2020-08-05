using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace MHWMasterDataUtils.Skills
{
    public class SkillAbilityPrimitive
    {
        public readonly uint Index;
        public readonly ushort SkillId;
        public readonly byte Level;
        public readonly IReadOnlyCollection<short> Params;

        private SkillAbilityPrimitive(
            uint index,
            ushort skillId,
            byte level,
            IReadOnlyCollection<short> parameters
        )
        {
            Index = index;
            SkillId = skillId;
            Level = level;
            Params = parameters;
        }

        public static SkillAbilityPrimitive Read(uint index, Reader reader)
        {
            ushort skillId = reader.ReadUInt16();
            byte level = reader.ReadByte();

            short[] parameters = new short[16];
            for (int i = 0; i < parameters.Length; i++)
                parameters[i] = reader.ReadInt16();

            return new SkillAbilityPrimitive(
                index,
                skillId,
                level,
                new ReadOnlyCollection<short>(parameters)
            );
        }
    }
}
