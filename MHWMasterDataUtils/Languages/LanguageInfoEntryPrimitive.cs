using System;
using System.Collections.Generic;
using System.Text;

namespace MHWMasterDataUtils.Languages
{
    public struct LanguageInfoEntryPrimitive
    {
        public readonly ulong KeyOffset;
        public readonly uint StringIndex;

        public LanguageInfoEntryPrimitive(ulong keyOffset, uint stringIndex)
        {
            KeyOffset = keyOffset;
            StringIndex = stringIndex;
        }

        public static LanguageInfoEntryPrimitive Read(Reader reader)
        {
            uint stringIndex = reader.ReadUInt32();
            reader.Offset(12); // Skip hash_key_2x, hash_key_3x and pad
            ulong keyOffset = reader.ReadUInt64();
            reader.Offset(8); // Skip bucket_index.

            return new LanguageInfoEntryPrimitive(keyOffset, stringIndex);
        }
    }
}
