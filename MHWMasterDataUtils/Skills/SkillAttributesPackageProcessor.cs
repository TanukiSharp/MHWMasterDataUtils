using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MHWMasterDataUtils.Skills
{
    public class SkillAbilitiesPackageProcessor : PackageProcessorBase
    {
        public Dictionary<ushort, Dictionary<byte, SkillAbilityPrimitive>> Table { get; } = new Dictionary<ushort, Dictionary<byte, SkillAbilityPrimitive>>();

        private static readonly ushort[] allowedHeaderValues = new ushort[] { 0x00BC };
        private static readonly ushort[] skipHeaderValues = new ushort[] { 0x0087, 0x00BB };

        public override bool IsChunkFileMatching(string chunkFullFilename)
        {
            return chunkFullFilename == "/common/equip/skill_data.skl_dat";
        }

        public override void PreProcess()
        {
            Table.Clear();
            base.PreProcess();
        }

        private uint ReadHeader(Reader reader)
        {
            PackageUtility.ReadAndAssertIceborneHeader(reader);
            PackageUtility.ReadAndAssertTwoBytesHeader(allowedHeaderValues, skipHeaderValues, reader);

            return reader.ReadUInt32();
        }

        public SkillAbilityPrimitive GetAbility(ushort skillId, byte level)
        {
            return Table[skillId][level];
        }

        private Dictionary<byte, SkillAbilityPrimitive> GetOrAddAbilitiesStorage(ushort skillId)
        {
            if (Table.TryGetValue(skillId, out Dictionary<byte, SkillAbilityPrimitive> storage) == false)
            {
                storage = new Dictionary<byte, SkillAbilityPrimitive>();
                Table.Add(skillId, storage);
            }

            return storage;
        }

        public override void ProcessChunkFile(Stream stream, string chunkFullFilename)
        {
            using var reader = new Reader(new BinaryReader(stream, Encoding.UTF8, true), chunkFullFilename);

            uint numEntries = ReadHeader(reader);

            for (uint i = 0; i < numEntries; i++)
            {
                var entry = SkillAbilityPrimitive.Read(i, reader);

                Dictionary<byte, SkillAbilityPrimitive> storage = GetOrAddAbilitiesStorage(entry.SkillId);

                if (storage.ContainsKey(entry.Level) == false)
                    storage.Add(entry.Level, entry);
            }
        }
    }
}
