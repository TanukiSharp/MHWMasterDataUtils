using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MHWMasterDataUtils.Core
{
    public class ArmorPiece : EquipmentBase
    {
        [JsonProperty("seriesId")]
        public int SeriesId { get; set; }
        [JsonProperty("defense")]
        public int Defense { get; set; }
        [JsonProperty("fireRes")]
        public int FireResistance { get; set; }
        [JsonProperty("waterRes")]
        public int WaterResistance { get; set; }
        [JsonProperty("thunderRes")]
        public int ThunderResistance { get; set; }
        [JsonProperty("iceRes")]
        public int IceResistance { get; set; }
        [JsonProperty("dragonRes")]
        public int DragonResistance { get; set; }
        [JsonProperty("slots")]
        public int[] Slots { get; set; }
    }
}
