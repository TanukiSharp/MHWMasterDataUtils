using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MHWMasterDataUtils.Core
{
    public class Item
    {
        [JsonProperty("id")]
        public uint Id { get; set; }
        [JsonProperty("name")]
        public Dictionary<string, string> Name { get; set; }
        [JsonProperty("description")]
        public Dictionary<string, string> Description { get; set; }
        [JsonProperty("rarity")]
        public uint Rarity { get; set; }
        [JsonProperty("buyPrice")]
        public uint BuyPrice { get; set; }
        [JsonProperty("sellPrice")]
        public uint SellPrice { get; set; }
        [JsonProperty("carryLimit")]
        public uint CarryLimit { get; set; }

        public override string ToString()
        {
            return $"{Name[LanguageUtils.DefaultLanguageCode]}";
        }
    }
}
