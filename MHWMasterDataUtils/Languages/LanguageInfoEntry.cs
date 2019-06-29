using System;
using System.Collections.Generic;
using System.Text;

namespace MHWMasterDataUtils.Languages
{
    public class LanguageInfoEntryPrimitive
    {
        public uint string_index;
        public ulong key_offset;
        public ulong bucket_index;

        public static LanguageInfoEntryPrimitive Read(Reader reader)
        {
            uint string_index = reader.ReadUInt32();
            reader.Offset(12); // Skip hash_key_2x, hash_key_3x and pad
            ulong key_offset = reader.ReadUInt64();
            ulong bucket_index = reader.ReadUInt64();

            return new LanguageInfoEntryPrimitive
            {
                string_index = string_index,
                key_offset = key_offset,
                bucket_index = bucket_index
            };
        }
    }
}
