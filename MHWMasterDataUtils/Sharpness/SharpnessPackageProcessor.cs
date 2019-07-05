using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MHWMasterDataUtils.Sharpness
{
    public class SharpnessPackageProcessor : PackageProcessorBase
    {
        public const ushort HeaderValue = 0x0177;
        public Dictionary<uint, SharpnessInfo> Table { get; } = new Dictionary<uint, SharpnessInfo>();

        private const int Id = 0;
        private const int Red = 1;
        private const int Orange = 2;
        private const int Yellow = 3;
        private const int Green = 1;
        private const int Blue = 1;
        private const int White = 1;
        private const int Purple = 1;

        public override Task PreProcess()
        {
            Table.Clear();
            return base.PreProcess();
        }

        public override bool IsChunkFileMatching(string chunkFullFilename)
        {
            return chunkFullFilename == "/common/equip/kireaji.kire";
        }

        private uint ReadHeader(Reader reader)
        {
            ushort headerValue = reader.ReadUInt16();

            if (headerValue != HeaderValue)
                throw new FormatException($"Invalid header in file '{reader.Filename ?? "<unknown>"}'. Expected {HeaderValue:x4}, read {headerValue:x4}.");

            return reader.ReadUInt32();
        }

        private static void ReadValues(Reader reader, ushort[] elements)
        {
            for (int i = 0; i < elements.Length; i++)
                elements[i] = reader.ReadUInt16();
        }

        public void TryAddSharpnessValues(ushort[] sharpnessValues)
        {
            if (Table.ContainsKey(sharpnessValues[Id]))
                return;

            var sharpnessInfo = new SharpnessInfo(
                sharpnessValues[Red],
                sharpnessValues[Orange],
                sharpnessValues[Yellow],
                sharpnessValues[Green],
                sharpnessValues[Blue],
                sharpnessValues[White],
                sharpnessValues[Purple]
            );

            Table.Add(sharpnessValues[0], sharpnessInfo);
        }

        public override Task ProcessChunkFile(Stream stream, string chunkFullFilename)
        {
            ushort[] sharpnessValues = new ushort[8];

            using (var reader = new Reader(new BinaryReader(stream, Encoding.UTF8, true), chunkFullFilename))
            {
                uint numEntries = ReadHeader(reader);

                for (uint i = 0; i < numEntries; i++)
                {
                    ReadValues(reader, sharpnessValues);
                    TryAddSharpnessValues(sharpnessValues);
                }
            }

            return Task.CompletedTask;
        }
    }
}
