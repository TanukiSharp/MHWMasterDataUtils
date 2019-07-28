using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MHWMasterDataUtils.Core
{
    public class Bowgun : WeaponBase
    {
        [JsonProperty("specialAmmo")]
        public BowgunSpecialAmmo SpecialAmmo { get; set;}
        [JsonProperty("deviation")]
        public BowgunDeviation Deviation { get; set; }
        [JsonProperty("ammos")]
        public Ammo[] Ammos { get; set; }
    }
}
