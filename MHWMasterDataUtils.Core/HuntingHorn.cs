using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MHWMasterDataUtils.Core
{
    public class HuntingHorn : MeleeWeapon
    {
        [JsonProperty("songs")]
        public HuntingHornSong[] Songs { get; set; }
    }
}
