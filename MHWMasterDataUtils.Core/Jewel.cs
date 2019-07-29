using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MHWMasterDataUtils.Core
{
    public class Jewel : Item
    {
        [JsonProperty("equipmentId")]
        public uint EquipementId { get; set; }
    }
}
