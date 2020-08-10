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
        [JsonProperty("elementalResistances")]
        public ElementalResistances ElementalResistances { get; set; }
        [JsonProperty("slots")]
        public int[] Slots { get; set; }
        [JsonProperty("setGroup")]
        public int SetGroup { get; set; }
        [JsonProperty("setSkills")]
        public int[] SetSkills { get; set; }
    }
}
