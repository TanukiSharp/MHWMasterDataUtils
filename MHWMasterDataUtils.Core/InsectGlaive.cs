using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MHWMasterDataUtils.Core
{
    public class InsectGlaive : MeleeWeapon
    {
        [JsonProperty("insectBonus")]
        public int InsectBonus { get; set; }
    }
}
