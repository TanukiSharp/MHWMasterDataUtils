using System;
using System.Collections.Generic;

namespace MHWMasterDataUtils.Core
{
    [System.Diagnostics.DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class LocalizedText : Dictionary<string, string>
    {
        private string DebuggerDisplay
        {
            get
            {
                return this[LanguageUtils.DefaultLanguageCode];
            }
        }
    }
}
