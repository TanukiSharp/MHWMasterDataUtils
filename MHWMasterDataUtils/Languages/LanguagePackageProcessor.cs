using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MHWMasterDataUtils.Languages
{
    public class LanguagePackageProcessor : PackageProcessorBase
    {
        public delegate bool FileMatchHandler(string packageFilename);

        public string RegexPattern { get; }
        private readonly Regex fileMatcher;

        public LanguagePackageProcessor(string regexPattern)
        {
            if (regexPattern == null)
                throw new ArgumentNullException(nameof(regexPattern));

            RegexPattern = regexPattern;

            fileMatcher = new Regex(regexPattern);
        }

        public override void PreProcess()
        {
            Table.Clear();
            base.PreProcess();
        }

        public override bool IsChunkFileMatching(string chunkFullFilename)
        {
            return fileMatcher.IsMatch(chunkFullFilename);
        }

        public Dictionary<LanguageIdPrimitive, Dictionary<uint, LanguageItem>> Table { get; } = new Dictionary<LanguageIdPrimitive, Dictionary<uint, LanguageItem>>();

        private void TryAddEntry(LanguageIdPrimitive languageId, uint index, string key, string value)
        {
            Dictionary<uint, LanguageItem> entries = GetOrAddLanguageEntries(languageId);

            if (entries.ContainsKey(index) == false)
                entries.Add(index, new LanguageItem(key, value));
        }

        private Dictionary<uint, LanguageItem> GetOrAddLanguageEntries(LanguageIdPrimitive languageId)
        {
            if (Table.TryGetValue(languageId, out Dictionary<uint, LanguageItem> entries) == false)
            {
                entries = new Dictionary<uint, LanguageItem>();
                Table.Add(languageId, entries);
            }

            return entries;
        }

        public override void ProcessChunkFile(Stream stream, string chunkFullFilename)
        {
            using (var reader = new Reader(new BinaryReader(stream, Encoding.UTF8, true), chunkFullFilename))
            {
                LanguageHeaderPrimitive header = LanguageHeaderPrimitive.Read(reader);

                LanguageInfoEntryPrimitive[] infoEntries = ReadInfoEntries(reader, header);

                // Skip buckets.
                reader.Offset(8 * 256);

                byte[] keyBlock = reader.ReadBytes((int)header.KeyBlockSize);
                byte[] stringBlock = reader.ReadBytes((int)header.StringBlockSize);

                List<string> values = ParseStringBlock(stringBlock);

                foreach (LanguageInfoEntryPrimitive infoEntry in infoEntries)
                {
                    int index = (int)infoEntry.KeyOffset;

                    string key = NativeUtils.GetNextString(keyBlock, ref index, Encoding.ASCII);
                    string value = values[(int)infoEntry.StringIndex];

                    TryAddEntry(header.LanguageId, infoEntry.StringIndex, key, value);
                }
            }
        }

        private static List<string> ParseStringBlock(byte[] stringBlock)
        {
            int index = 0;
            var values = new List<string>();

            while (index < stringBlock.Length)
                values.Add(NativeUtils.GetNextString(stringBlock, ref index, Encoding.UTF8));

            return values;
        }

        private static LanguageInfoEntryPrimitive[] ReadInfoEntries(Reader reader, LanguageHeaderPrimitive header)
        {
            var infoEntries = new LanguageInfoEntryPrimitive[header.KeyCount];
            for (uint i = 0; i < header.KeyCount; i++)
                infoEntries[i] = LanguageInfoEntryPrimitive.Read(reader);
            return infoEntries;
        }

        public override string ToString()
        {
            return $"[GMD] {RegexPattern}";
        }
    }
}
