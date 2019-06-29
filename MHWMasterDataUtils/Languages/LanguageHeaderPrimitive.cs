using System;
using System.Collections.Generic;
using System.Text;

namespace MHWMasterDataUtils.Languages
{
    public class LanguageHeaderPrimitive
    {
        public uint magic;
        public uint version;
        public LanguageIdPrimitive lang_id;
        public uint key_count;
        public uint string_count;
        public uint key_block_size;
        public uint string_block_size;
        public uint name_size;
        public string name; // char name[name_size + 1]; // null byte terminated string

        public const uint MagicNumber = 0x00444d47;

        public static LanguageHeaderPrimitive Read(Reader reader)
        {
            uint magic = reader.ReadUInt32();

            if (magic != MagicNumber)
                throw new FormatException($"Invalid magic number in file '{reader.Filename ?? "<unknown>"}'. Expected {MagicNumber:x8}, read {magic:x8}.");

            uint version = reader.ReadUInt32();
            var lang_id = (LanguageIdPrimitive)reader.ReadUInt32();
            reader.Offset(8); // Skip unknown1 and unknown2.
            uint key_count = reader.ReadUInt32();
            uint string_count = reader.ReadUInt32();
            uint key_block_size = reader.ReadUInt32();
            uint string_block_size = reader.ReadUInt32();
            uint name_size = reader.ReadUInt32();
            byte[] name = reader.ReadBytes((int)name_size + 1);

            return new LanguageHeaderPrimitive
            {
                magic = magic,
                version = version,
                lang_id = lang_id,
                key_count = key_count,
                string_count = string_count,
                key_block_size = key_block_size,
                string_block_size = string_block_size,
                name_size = name_size,
                name = Encoding.UTF8.GetString(name, 0, name.Length - 1)
            };
        }
    }
}
