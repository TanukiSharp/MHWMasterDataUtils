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
    }
}
