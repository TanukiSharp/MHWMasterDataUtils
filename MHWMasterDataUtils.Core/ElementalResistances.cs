using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MHWMasterDataUtils.Core
{
    public class ElementalResistances
    {
        [JsonProperty("fire")]
        public int Fire { get; set; }
        [JsonProperty("water")]
        public int Water { get; set; }
        [JsonProperty("thunder")]
        public int Thunder{ get; set; }
        [JsonProperty("ice")]
        public int Ice { get; set; }
        [JsonProperty("dragon")]
        public int Dragon { get; set; }
    }
}
