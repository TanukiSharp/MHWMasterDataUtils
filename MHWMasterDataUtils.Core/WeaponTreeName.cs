using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MHWMasterDataUtils.Core
{
    public class WeaponTreeName
    {
        [JsonProperty("treeId")]
        public int TreeId { get; set; }
        [JsonProperty("name")]
        public Dictionary<string, string> Name { get; set; }
    }
}
