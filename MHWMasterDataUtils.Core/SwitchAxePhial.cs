using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MHWMasterDataUtils.Core
{
    public class SwitchAxePhial
    {
        [JsonProperty("type")]
        public SwitchAxePhialType Type { get; set; }
        [JsonProperty("damage")]
        public int? Damage { get; set; }
    }
}
