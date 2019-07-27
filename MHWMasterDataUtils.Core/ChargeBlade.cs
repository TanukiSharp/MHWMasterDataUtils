using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MHWMasterDataUtils.Core
{
    public class ChargeBlade : MeleeWeapon
    {
        [JsonProperty("phialType")]
        public ChargeBladePhialType PhialType { get; set; }
    }
}
