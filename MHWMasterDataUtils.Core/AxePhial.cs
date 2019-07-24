using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MHWMasterDataUtils.Core
{
    public class AxePhial
    {
        [JsonProperty("elementStatus")]
        public int ElementStatus { get; set; }
        [JsonProperty("damage")]
        public int Damage { get; set; }
    }
}
