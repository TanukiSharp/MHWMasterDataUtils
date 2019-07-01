using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MHWMasterDataUtils.Items
{
    public class ItemsPackageProcessor : PackageProcessorBase
    {
        public override bool IsChunkFileMatching(string chunkFullFilename)
        {
            return chunkFullFilename == @"\common\item\itemData.itm";
        }

        public const ushort HeaderIdentifier = 0x00AE;

        public readonly Dictionary<uint, ItemEntryPrimitive> Table = new Dictionary<uint, ItemEntryPrimitive>();

        public override Task PreProcess()
        {
            Table.Clear();
            return base.PreProcess();
        }

        public override Task ProcessChunkFile(Stream stream, string chunkFullFilename)
        {
            using (var reader = new Reader(new BinaryReader(stream, Encoding.UTF8, true), chunkFullFilename))
            {
                ushort identifier = reader.ReadUInt16();

                if (identifier != HeaderIdentifier)
                    throw new FormatException($"Invalid header identifier in file '{reader.Filename ?? "<unknown>"}'. Expected {HeaderIdentifier:x4}, read {identifier:x4}.");

                uint num_entries = reader.ReadUInt32();

                for (uint i = 0; i < num_entries; i++)
                {
                    ItemEntryPrimitive itemEntry = ItemEntryPrimitive.Read(reader);

                    if (Table.ContainsKey(itemEntry.id) == false)
                        Table.Add(itemEntry.id, itemEntry);
                    else
                    {
                    }
                }
            }

            return Task.CompletedTask;
        }
    }
}
