using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

using core = MHWMasterDataUtils.Core;

namespace MHWMasterDataUtils.Equipment
{
    public class EquipmentUpgradePackageProcessor : PackageProcessorBase
    {
        public string MatchingChunkFullFilename { get; }
        public Dictionary<byte, Dictionary<ushort, EquipmentUpgradeEntryPrimitive>> Table { get; } = new Dictionary<byte, Dictionary<ushort, EquipmentUpgradeEntryPrimitive>>();

        private const ushort headerValue = 0x0051;

        public EquipmentUpgradePackageProcessor(string matchingChunkFullFilename)
        {
            MatchingChunkFullFilename = matchingChunkFullFilename;
        }

        public override bool IsChunkFileMatching(string chunkFullFilename)
        {
            return chunkFullFilename == MatchingChunkFullFilename;
        }

        public override void PreProcess()
        {
            Table.Clear();
            base.PreProcess();
        }

        private uint ReadHeader(Reader reader)
        {
            ushort headerValue = reader.ReadUInt16();

            if (headerValue != EquipmentUpgradePackageProcessor.headerValue)
                throw new FormatException($"Invalid header in file '{reader.Filename ?? "<unknown>"}'. Expected {EquipmentUpgradePackageProcessor.headerValue:x4}, read {headerValue:x4}.");

            return reader.ReadUInt32();
        }

        public static bool HasDescendants(EquipmentUpgradeEntryPrimitive entry)
        {
            return entry.Descendant1Id > 0 || entry.Descendant2Id > 0 || entry.Descendant3Id > 0 || entry.Descendant4Id > 0;
        }

        public override void ProcessChunkFile(Stream stream, string chunkFullFilename)
        {
            using (var reader = new Reader(new BinaryReader(stream, Encoding.UTF8, true), chunkFullFilename))
            {
                uint numEntries = ReadHeader(reader);

                for (uint i = 0; i < numEntries; i++)
                {
                    var entry = EquipmentUpgradeEntryPrimitive.Read(reader);

                    if (Table.TryGetValue(entry.EquipType, out Dictionary<ushort, EquipmentUpgradeEntryPrimitive> perWeaponEntries) == false)
                    {
                        perWeaponEntries = new Dictionary<ushort, EquipmentUpgradeEntryPrimitive>();
                        Table.Add(entry.EquipType, perWeaponEntries);
                    }

                    if (perWeaponEntries.TryGetValue(entry.EquipId, out EquipmentUpgradeEntryPrimitive existingEntry) == false || HasDescendants(existingEntry) == false)
                        perWeaponEntries[entry.EquipId] = entry;
                }
            }
        }
    }
}
