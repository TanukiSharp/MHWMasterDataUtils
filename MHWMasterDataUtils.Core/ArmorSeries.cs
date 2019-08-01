using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MHWMasterDataUtils.Core
{
    public class ArmorSeries
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public Dictionary<string, string> Name { get; set; }
    }
}
