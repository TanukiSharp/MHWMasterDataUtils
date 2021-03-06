﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MHWMasterDataUtils.Equipment
{
    public class EquipmentCraftPackageProcessor<TEquipmentType> : PackageProcessorBase
    {
        public Dictionary<TEquipmentType, Dictionary<uint, EquipmentCraftEntryPrimitive>> Table { get; } = new Dictionary<TEquipmentType, Dictionary<uint, EquipmentCraftEntryPrimitive>>();

        private readonly ushort[] headerValues = new ushort[] { 0x0079 };

        static EquipmentCraftPackageProcessor()
        {
            if (typeof(TEquipmentType).IsEnum == false)
                throw new InvalidOperationException($"Type argument '{nameof(TEquipmentType)}' must be of type enum.");
        }

        public EquipmentCraftEntryPrimitive GetEntry(TEquipmentType equipmentType, uint equipmentId)
        {
            return Table[equipmentType][equipmentId];
        }

        public bool TryGetEntry(TEquipmentType equipmentType, uint equipmentId, out EquipmentCraftEntryPrimitive entry)
        {
            if (Table.TryGetValue(equipmentType, out Dictionary<uint, EquipmentCraftEntryPrimitive> entries) == false)
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

        public EquipmentCraftPackageProcessor(string matchingChunkFullFilename)
            : this(new[] { matchingChunkFullFilename })
        {
        }

        public EquipmentCraftPackageProcessor(IEnumerable<string> matchingChunkFullFilenames)
        {
            this.matchingChunkFullFilenames = matchingChunkFullFilenames;
        }

        public override void PreProcess()
        {
            base.PreProcess();
        }

        private uint ReadHeader(Reader reader)
        {
            PackageUtility.ReadAndAssertIceborneHeader(reader);
            PackageUtility.ReadAndAssertTwoBytesHeader(headerValues, reader);

            return reader.ReadUInt32();
        }

        private Dictionary<uint, EquipmentCraftEntryPrimitive> GetOrAddCraftEntriesStorage(TEquipmentType equipmentType)
        {
            if (Table.TryGetValue(equipmentType, out Dictionary<uint, EquipmentCraftEntryPrimitive> storage) == false)
            {
                storage = new Dictionary<uint, EquipmentCraftEntryPrimitive>();
                Table.Add(equipmentType, storage);
            }

            return storage;
        }

        public override void ProcessChunkFile(Stream stream, string chunkFullFilename)
        {
            using var reader = new Reader(new BinaryReader(stream, Encoding.UTF8, true), chunkFullFilename);

            uint numEntries = ReadHeader(reader);

            for (uint i = 0; i < numEntries; i++)
            {
                var entry = EquipmentCraftEntryPrimitive.Read(reader);

                Dictionary<uint, EquipmentCraftEntryPrimitive> storage = GetOrAddCraftEntriesStorage((TEquipmentType)Enum.ToObject(typeof(TEquipmentType), entry.EquipType));

                if (storage.ContainsKey(entry.EquipId) == false)
                    storage.Add(entry.EquipId, entry);
            }
        }
    }
}
