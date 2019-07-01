using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MHWMasterDataUtils.Sharpness
{
    public class SharpnessPackageProcessor : PackageProcessorBase
    {
        public override bool IsChunkFileMatching(string chunkFullFilename)
        {
            return chunkFullFilename == @"\common\equip\kireaji.kire";
        }

        public const ushort HeaderIdentifier = 0x0177;

        public readonly Dictionary<uint, SharpnessInfo> Table = new Dictionary<uint, SharpnessInfo>();

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

                ushort[] elements = new ushort[8];

                for (uint i = 0; i < num_entries; i++)
                {
                    for (int j = 0; j < elements.Length; j++)
                        elements[j] = reader.ReadUInt16();

                    if (Table.ContainsKey(elements[0]) == false)
                        Table.Add(elements[0], new SharpnessInfo(elements[1], elements[2], elements[3], elements[4], elements[5], elements[6], elements[7]));
                }
            }

            return Task.CompletedTask;
        }
    }
}
