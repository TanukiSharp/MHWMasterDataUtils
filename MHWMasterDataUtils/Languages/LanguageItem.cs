using System;
using System.Collections.Generic;
using System.Text;

namespace MHWMasterDataUtils.Languages
{
    public struct LanguageItem
    {
        public readonly uint Index;
        public readonly string Key;
        public readonly string Value;

        public LanguageItem(uint index, string key, string value)
        {
            Index = index;
            Key = key;
            Value = value;
        }

        public override string ToString()
        {
            return $"[{Index}] '{Key}' = '{Value}'";
        }
    }
}
