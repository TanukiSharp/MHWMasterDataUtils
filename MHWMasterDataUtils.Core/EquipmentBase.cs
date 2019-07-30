using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MHWMasterDataUtils.Core
{
    public abstract class EquipmentBase
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("order")]
        public int Order { get; set; }
        [JsonProperty("name")]
        public Dictionary<string, string> Name { get; set; }
        [JsonProperty("description")]
        public Dictionary<string, string> Description { get; set; }
    }
}
