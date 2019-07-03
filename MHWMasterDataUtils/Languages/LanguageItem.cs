using System;
using System.Collections.Generic;
using System.Text;

namespace MHWMasterDataUtils.Languages
{
    public struct LanguageItem
    {
        public readonly string Key;
        public readonly string Value;

        public LanguageItem(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public override string ToString()
        {
            return $"'{Key}' = '{Value}'";
        }
    }
}
