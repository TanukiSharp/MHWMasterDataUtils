using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MHWMasterDataUtils.Core
{
    public class Ammo
    {
        [JsonProperty("type")]
        public AmmoType Type { get; set; }
        [JsonProperty("capacity")]
        public int Capacity { get; set; }
        [JsonProperty("shotType")]
        public AmmoShotType ShotType { get; set; }
        [JsonProperty("reload")]
        public AmmoReload Reload { get; set; }

        public override string ToString()
        {
            return $"{Type}   {Capacity}   {ShotType}   {Reload}";
        }
    }
}
