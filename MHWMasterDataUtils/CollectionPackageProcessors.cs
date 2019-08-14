﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MHWMasterDataUtils
{
    public abstract class ListPackageProcessorBase<TValue> : PackageProcessorBase
    {
        public ushort HeaderValue { get; }
        public List<TValue> List { get; } = new List<TValue>();

        private readonly Func<Reader, TValue> entryReader;

        protected ListPackageProcessorBase(ushort headerValue, Func<Reader, TValue> entryReader)
        {
            HeaderValue = headerValue;

            this.entryReader = entryReader;
        }

        public override void PreProcess()
        {
            List.Clear();
            base.PreProcess();
        }

        private uint ReadHeader(Reader reader)
        {
            ushort headerValue = reader.ReadUInt16();

            if (headerValue != HeaderValue)
                throw new FormatException($"Invalid header in file '{reader.Filename ?? "<unknown>"}'. Expected {HeaderValue:x4}, read {headerValue:x4}.");

            return reader.ReadUInt32();
        }

        public override void ProcessChunkFile(Stream stream, string chunkFullFilename)
        {
            using (var reader = new Reader(new BinaryReader(stream, Encoding.UTF8, true), chunkFullFilename))
            {
                uint numEntries = ReadHeader(reader);

                for (uint i = 0; i < numEntries; i++)
                {
                    TValue entry = entryReader(reader);
                    List.Add(entry);
                }
            }
        }
    }

    public abstract class MapPackageProcessorBase<TKey, TValue> : PackageProcessorBase
    {
        public ushort HeaderValue { get; }
        public Dictionary<TKey, TValue> Table { get; } = new Dictionary<TKey, TValue>();

        private readonly Func<Reader, TValue> entryReader;
        private readonly Func<TValue, TKey> keySelector;

        protected MapPackageProcessorBase(ushort headerValue, Func<Reader, TValue> entryReader, Func<TValue, TKey> keySelector)
        {
            HeaderValue = headerValue;

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
            ushort headerValue = reader.ReadUInt16();

            if (headerValue != HeaderValue)
                throw new FormatException($"Invalid header in file '{reader.Filename ?? "<unknown>"}'. Expected {HeaderValue:x4}, read {headerValue:x4}.");

            return reader.ReadUInt32();
        }

        public override void ProcessChunkFile(Stream stream, string chunkFullFilename)
        {
            using (var reader = new Reader(new BinaryReader(stream, Encoding.UTF8, true), chunkFullFilename))
            {
                uint numEntries = ReadHeader(reader);

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
}