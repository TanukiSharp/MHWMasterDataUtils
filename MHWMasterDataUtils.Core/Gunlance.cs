using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MHWMasterDataUtils.Core
{
    public class Gunlance : MeleeWeapon
    {
        [JsonProperty("shelling")]
        public GunlanceShell Shelling { get; set; }
    }
}
