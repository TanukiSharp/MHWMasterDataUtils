using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MHWMasterDataUtils.Core
{
    public class DualBlades : MeleeWeapon
    {
        [JsonProperty("secondaryElementStatus")]
        public ElementStatus? SecondaryElementStatus { get; set; }
        [JsonProperty("secondaryElementStatusDamage")]
        public int? SecondaryElementStatusDamage { get; set; }
    }
}
