using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MHWMasterDataUtils.Core;

namespace MHWMasterDataUtils.Sharpness
{
    public class SharpnessPackageProcessor : PackageProcessorBase
    {
        public Dictionary<uint, SharpnessInfo> Table { get; } = new Dictionary<uint, SharpnessInfo>();

        private static readonly ushort[] headerValues = new ushort[] { 0x01C1 };

        private const int Red = 0;
        private const int Orange = 1;
        private const int Yellow = 2;
        private const int Green = 3;
        private const int Blue = 4;
        private const int White = 5;
        private const int Purple = 6;

        public override void PreProcess()
        {
            Table.Clear();
            base.PreProcess();
        }

        public override bool IsChunkFileMatching(string chunkFullFilename)
        {
            return chunkFullFilename == "/common/equip/kireaji.kire";
        }

        private uint ReadHeader(Reader reader)
        {
            PackageUtility.ReadAndAssertIceborneHeader(reader);
            PackageUtility.ReadAndAssertTwoBytesHeader(headerValues, reader);

            return reader.ReadUInt32();
        }

        private static uint ReadValues(Reader reader, ushort[] elements)
        {
            uint id = reader.ReadUInt32();

            for (int i = 0; i < elements.Length; i++)
                elements[i] = reader.ReadUInt16();

            return id;
        }

        public void TryAddSharpnessValues(uint id, ushort[] sharpnessValues)
        {
            if (Table.ContainsKey(id))
                return;

            var sharpnessInfo = SharpnessInfo.FromAbsoluteValues(
                sharpnessValues[Red],
                sharpnessValues[Orange],
                sharpnessValues[Yellow],
                sharpnessValues[Green],
                sharpnessValues[Blue],
                sharpnessValues[White],
                sharpnessValues[Purple]
            );

            Table.Add(id, sharpnessInfo);
        }

        public override void ProcessChunkFile(Stream stream, string chunkFullFilename)
        {
            ushort[] sharpnessValues = new ushort[7];

            using var reader = new Reader(new BinaryReader(stream, Encoding.UTF8, true), chunkFullFilename);

            uint numEntries = ReadHeader(reader);

            for (uint i = 0; i < numEntries; i++)
            {
                uint id = ReadValues(reader, sharpnessValues);
                TryAddSharpnessValues(id, sharpnessValues);
            }
        }
    }
}
