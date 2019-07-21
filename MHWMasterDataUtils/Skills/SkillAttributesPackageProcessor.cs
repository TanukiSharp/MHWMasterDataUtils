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

        private readonly ushort headerValue = 0x5e;

        public override bool IsChunkFileMatching(string chunkFullFilename)
        {
            return chunkFullFilename == "/common/equip/skill_data.skl_dat";
        }

        public override Task PreProcess()
        {
            Table.Clear();
            return base.PreProcess();
        }

        private uint ReadHeader(Reader reader)
        {
            ushort headerValue = reader.ReadUInt16();

            if (headerValue != this.headerValue)
                throw new FormatException($"Invalid header in file '{reader.Filename ?? "<unknown>"}'. Expected {this.headerValue:x4}, read {headerValue:x4}.");

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

        public override Task ProcessChunkFile(Stream stream, string chunkFullFilename)
        {
            using (var reader = new Reader(new BinaryReader(stream, Encoding.UTF8, true), chunkFullFilename))
            {
                uint numEntries = ReadHeader(reader);

                for (uint i = 0; i < numEntries; i++)
                {
                    var entry = SkillAbilityPrimitive.Read(i, reader);

                    Dictionary<byte, SkillAbilityPrimitive> storage = GetOrAddAbilitiesStorage(entry.SkillId);

                    if (storage.ContainsKey(entry.Level) == false)
                        storage.Add(entry.Level, entry);
                }
            }

            return Task.CompletedTask;
        }
    }
}
