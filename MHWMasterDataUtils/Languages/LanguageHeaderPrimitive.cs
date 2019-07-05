using System;
using System.Collections.Generic;
using System.Text;

namespace MHWMasterDataUtils.Languages
{
    public class LanguageHeaderPrimitive
    {
        public readonly uint HeaderValue;
        public readonly uint Version;
        public readonly LanguageIdPrimitive LanguageId;
        public readonly uint KeyCount;
        public readonly uint StringCount;
        public readonly uint KeyBlockSize;
        public readonly uint StringBlockSize;
        public readonly uint NameSize;
        public readonly string Name; // char name[name_size + 1]; // null byte terminated string

        private const uint expectedHeaderValue = 0x00444d47;

        private LanguageHeaderPrimitive(
            uint headerValue,
            uint version,
            LanguageIdPrimitive languageId,
            uint keyCount,
            uint stringCount,
            uint keyBlockSize,
            uint stringBlockSize,
            uint nameSize,
            string name
        )
        {
            HeaderValue = headerValue;
            Version = version;
            LanguageId = languageId;
            KeyCount = keyCount;
            StringCount = stringCount;
            KeyBlockSize = keyBlockSize;
            StringBlockSize = stringBlockSize;
            NameSize = nameSize;
            Name = name;
        }

        public static LanguageHeaderPrimitive Read(Reader reader)
        {
            uint headerValue = reader.ReadUInt32();

            if (headerValue != expectedHeaderValue)
                throw new FormatException($"Invalid magic number in file '{reader.Filename ?? "<unknown>"}'. Expected {expectedHeaderValue:x8}, read {headerValue:x8}.");

            uint version = reader.ReadUInt32();
            var languageId = (LanguageIdPrimitive)reader.ReadUInt32();
            reader.Offset(8); // Skip unknown1 and unknown2.
            uint keyCount = reader.ReadUInt32();
            uint stringCount = reader.ReadUInt32();
            uint keyBlockSize = reader.ReadUInt32();
            uint stringBlockSize = reader.ReadUInt32();
            uint nameSize = reader.ReadUInt32();
            byte[] name = reader.ReadBytes((int)nameSize + 1);

            return new LanguageHeaderPrimitive(
                headerValue,
                version,
                languageId,
                keyCount,
                stringCount,
                keyBlockSize,
                stringBlockSize,
                nameSize,
                Encoding.UTF8.GetString(name, 0, name.Length - 1)
            );
        }
    }
}
