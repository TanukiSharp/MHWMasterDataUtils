using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MHWMasterDataUtils.Crafting
{
    public class CraftPackageProcessor<TEquipmentType> : PackageProcessorBase
    {
        public Dictionary<TEquipmentType, Dictionary<uint, CraftEntryPrimitive>> Table { get; } = new Dictionary<TEquipmentType, Dictionary<uint, CraftEntryPrimitive>>();

        private readonly ushort headerValue = 0x51;

        static CraftPackageProcessor()
        {
            if (typeof(TEquipmentType).IsEnum == false)
                throw new InvalidOperationException($"Type argument '{nameof(TEquipmentType)}' must be of type enum.");
        }

        public CraftEntryPrimitive GetEntry(TEquipmentType equipmentType, uint equipmentId)
        {
            return Table[equipmentType][equipmentId];
        }

        public bool TryGetEntry(TEquipmentType equipmentType, uint equipmentId, out CraftEntryPrimitive entry)
        {
            if (Table.TryGetValue(equipmentType, out Dictionary<uint, CraftEntryPrimitive> entries) == false)
            {
                entry = default;
                return false;
            }

            return entries.TryGetValue(equipmentId, out entry);
        }

        private readonly IEnumerable<string> matchingChunkFullFilenames;

        public override bool IsChunkFileMatching(string chunkFullFilename)
        {
            foreach (string matchingChunkFullFilename in matchingChunkFullFilenames)
            {
                if (string.Equals(matchingChunkFullFilename, chunkFullFilename, StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            return false;
        }

        public CraftPackageProcessor(string matchingChunkFullFilename)
            : this(new[] { matchingChunkFullFilename })
        {
        }

        public CraftPackageProcessor(IEnumerable<string> matchingChunkFullFilenames)
        {
            this.matchingChunkFullFilenames = matchingChunkFullFilenames;
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

        private Dictionary<uint, CraftEntryPrimitive> GetOrAddCraftEntriesStorage(TEquipmentType equipmentType)
        {
            if (Table.TryGetValue(equipmentType, out Dictionary<uint, CraftEntryPrimitive> storage) == false)
            {
                storage = new Dictionary<uint, CraftEntryPrimitive>();
                Table.Add(equipmentType, storage);
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
                    var entry = CraftEntryPrimitive.Read(reader);

                    Dictionary<uint, CraftEntryPrimitive> storage = GetOrAddCraftEntriesStorage((TEquipmentType)Enum.ToObject(typeof(TEquipmentType), entry.EquipType));

                    if (storage.ContainsKey(entry.EquipId) == false)
                        storage.Add(entry.EquipId, entry);
                }
            }

            return Task.CompletedTask;
        }
    }
}
