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
        public Dictionary<byte, Dictionary<uint, EquipmentUpgradeEntryPrimitive>> Table { get; } = new Dictionary<byte, Dictionary<uint, EquipmentUpgradeEntryPrimitive>>();

        private static readonly ushort[] headerValues = new ushort[] { 0x0058 };

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
            PackageUtility.ReadAndAssertIceborneHeader(reader);
            PackageUtility.ReadAndAssertTwoBytesHeader(headerValues, reader);

            return reader.ReadUInt32();
        }

        public static bool HasDescendants(EquipmentUpgradeEntryPrimitive entry)
        {
            return entry.Descendant1Id > 0 || entry.Descendant2Id > 0 || entry.Descendant3Id > 0 || entry.Descendant4Id > 0;
        }

        public override void ProcessChunkFile(Stream stream, string chunkFullFilename)
        {
            using var reader = new Reader(new BinaryReader(stream, Encoding.UTF8, true), chunkFullFilename);

            uint numEntries = ReadHeader(reader);

            for (uint i = 0; i < numEntries; i++)
            {
                var entry = EquipmentUpgradeEntryPrimitive.Read(reader);

                if (Table.TryGetValue(entry.EquipType, out Dictionary<uint, EquipmentUpgradeEntryPrimitive> perWeaponEntries) == false)
                {
                    perWeaponEntries = new Dictionary<uint, EquipmentUpgradeEntryPrimitive>();
                    Table.Add(entry.EquipType, perWeaponEntries);
                }

                if (perWeaponEntries.TryGetValue(entry.EquipId, out EquipmentUpgradeEntryPrimitive existingEntry) == false || HasDescendants(existingEntry) == false)
                    perWeaponEntries[entry.EquipId] = entry;
            }
        }
    }
}
