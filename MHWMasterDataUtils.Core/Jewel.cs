using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MHWMasterDataUtils.Core
{
    public class Jewel : Item
    {
        [JsonProperty("equipmentId")]
        public uint EquipmentId { get; set; }
        [JsonProperty("skills")]
        public EquipmentSkill[] Skills { get; set; }
    }
}
