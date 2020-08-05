using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MHWMasterDataUtils
{
    public abstract class ListPackageProcessorBase<TValue> : PackageProcessorBase
    {
        public List<TValue> List { get; } = new List<TValue>();

        private readonly ushort[] headerValues;
        private readonly Func<Reader, TValue> entryReader;

        protected ListPackageProcessorBase(ushort[] headerValues, Func<Reader, TValue> entryReader)
        {
            this.headerValues = headerValues;

            this.entryReader = entryReader;
        }

        public override void PreProcess()
        {
            List.Clear();
            base.PreProcess();
        }

        private uint ReadHeader(Reader reader)
        {
            PackageUtility.ReadAndAssertIceborneHeader(reader);
            PackageUtility.ReadAndAssertTwoBytesHeader(headerValues, reader);

            return reader.ReadUInt32();
        }

        public override void ProcessChunkFile(Stream stream, string chunkFullFilename)
        {
            using var reader = new Reader(new BinaryReader(stream, Encoding.UTF8, true), chunkFullFilename);

            uint numEntries = ReadHeader(reader);

            for (uint i = 0; i < numEntries; i++)
            {
                TValue entry = entryReader(reader);
                List.Add(entry);
            }
        }
    }

    public abstract class MapPackageProcessorBase<TKey, TValue> : PackageProcessorBase
    {
        public Dictionary<TKey, TValue> Table { get; } = new Dictionary<TKey, TValue>();

        private readonly ushort[] allowedHeaderValues;
        private readonly ushort[] ignoredHeaderValues;
        private readonly Func<Reader, TValue> entryReader;
        private readonly Func<TValue, TKey> keySelector;

        protected MapPackageProcessorBase(ushort[] allowedHeaderValues, Func<Reader, TValue> entryReader, Func<TValue, TKey> keySelector)
            : this(allowedHeaderValues, null, entryReader, keySelector)
        {
        }

        protected MapPackageProcessorBase(ushort[] allowedHeaderValues, ushort[] ignoredHeaderValues, Func<Reader, TValue> entryReader, Func<TValue, TKey> keySelector)
        {
            this.allowedHeaderValues = allowedHeaderValues;
            this.ignoredHeaderValues = ignoredHeaderValues;

            this.entryReader = entryReader;
            this.keySelector = keySelector;
        }

        public override void PreProcess()
        {
            Table.Clear();
            base.PreProcess();
        }

        private uint ReadHeader(Reader reader)
        {
            PackageUtility.ReadAndAssertIceborneHeader(reader);
            PackageUtility.ReadAndAssertTwoBytesHeader(allowedHeaderValues, ignoredHeaderValues, reader);

            return reader.ReadUInt16();
        }

        public override void ProcessChunkFile(Stream stream, string chunkFullFilename)
        {
            using var reader = new Reader(new BinaryReader(stream, Encoding.UTF8, true), chunkFullFilename);

            uint numEntries = ReadHeader(reader);

            reader.Offset(2);

            for (uint i = 0; i < numEntries; i++)
            {
                TValue entry = entryReader(reader);
                TKey key = keySelector(entry);

                if (Table.ContainsKey(key) == false)
                    Table.Add(key, entry);
            }
        }
    }
}
