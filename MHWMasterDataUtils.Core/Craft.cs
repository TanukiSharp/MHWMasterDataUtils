using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MHWMasterDataUtils.Core
{
    public class Craft
    {
        [JsonProperty("isCraftable")]
        public bool IsCraftable { get; set; }
        [JsonProperty("items")]
        public CraftItem[] Items { get; set; }
    }

    public class CraftItem
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("quantity")]
        public int Quantity { get; set; }
    }
}
