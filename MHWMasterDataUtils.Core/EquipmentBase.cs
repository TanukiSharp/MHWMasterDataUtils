using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MHWMasterDataUtils.Core
{
    public abstract class EquipmentBase
    {
        [JsonProperty("id")]
        public uint Id { get; set; }
        [JsonProperty("order")]
        public int Order { get; set; }
        [JsonProperty("name")]
        public Dictionary<string, string> Name { get; set; }
        [JsonProperty("description")]
        public Dictionary<string, string> Description { get; set; }
        [JsonProperty("cost")]
        public uint Cost { get; set; }
        [JsonProperty("gender")]
        public Gender Gender { get; set; }
        [JsonProperty("rarity")]
        public int Rarity { get; set; }
        [JsonProperty("craft")]
        public CraftItem[] Craft { get; set; }
        [JsonProperty("isPermanent")]
        public bool IsPermanent { get; set; }

        public override string ToString()
        {
            return $"{Id} {Name} (order: {Order})";
        }
    }
}
