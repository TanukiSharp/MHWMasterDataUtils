using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MHWMasterDataUtils.Core
{
    public class SwitchAxe : MeleeWeapon
    {
        [JsonProperty("phial")]
        public SwitchAxePhial Phial { get; set; }
    }
}
