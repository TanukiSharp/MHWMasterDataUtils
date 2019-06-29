using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MHWMasterDataUtils.Languages
{
    public class LanguagePackageFileProcessor : PackageProcessorBase
    {
        public delegate bool FileMatchHandler(string packageFilename);

        private readonly FileMatchHandler fileMatcher;

        public LanguagePackageFileProcessor(FileMatchHandler fileMatcher)
        {
            if (fileMatcher == null)
                throw new ArgumentNullException(nameof(fileMatcher));

            this.fileMatcher = fileMatcher;
        }

        public override Task PreProcess()
        {
            Table.Clear();
            return base.PreProcess();
        }

        public override Task PrePackageFileProcess(string packageFullFilename)
        {
            return base.PrePackageFileProcess(packageFullFilename);
        }

        public override Task PreChunkFileProcess(string chunkFullFilename)
        {
            return base.PreChunkFileProcess(chunkFullFilename);
        }

        public override bool IsChunkFileMatching(string chunkFullFilename)
        {
            return fileMatcher(chunkFullFilename);
        }

        public Dictionary<LanguageIdPrimitive, Dictionary<string, string>> Table = new Dictionary<LanguageIdPrimitive, Dictionary<string, string>>();

        private string GetNextString(byte[] buffer, ref int index)
        {
            int start = index;

            while (index < buffer.Length && buffer[index] != 0)
                index++;

            index++;

            return Encoding.UTF8.GetString(buffer, start, index - start - 1);
        }

        public override Task ProcessChunkFile(Stream stream, string chunkFullFilename)
        {
            using (var reader = new Reader(new BinaryReader(stream, Encoding.UTF8, true), chunkFullFilename))
            {
                LanguageHeaderPrimitive header = LanguageHeaderPrimitive.Read(reader);

                var infoEntries = new LanguageInfoEntryPrimitive[header.key_count];
                for (uint i = 0; i < header.key_count; i++)
                    infoEntries[i] = LanguageInfoEntryPrimitive.Read(reader);

                var buckets = new ulong[256];
                for (int i = 0; i < buckets.Length; i++)
                    buckets[i] = reader.ReadUInt64();

                byte[] key_block = reader.ReadBytes((int)header.key_block_size);
                byte[] string_block = reader.ReadBytes((int)header.string_block_size);

                var values = new List<string>();
                int index = 0;
                while (index < string_block.Length)
                    values.Add(GetNextString(string_block, ref index));

                foreach (LanguageInfoEntryPrimitive infoEntry in infoEntries)
                {
                    index = (int)infoEntry.key_offset;
                    string key = GetNextString(key_block, ref index);
                    string value = values[(int)infoEntry.string_index];

                    if (Table.TryGetValue(header.lang_id, out Dictionary<string, string> entries) == false)
                    {
                        entries = new Dictionary<string, string>();
                        Table.Add(header.lang_id, entries);
                    }

                    if (entries.ContainsKey(key) == false)
                        entries.Add(key, value);
                    else
                    {
                        if (value != entries[key])
                        {
                        }
                    }
                }
            }

            return Task.CompletedTask;
        }
    }
}
