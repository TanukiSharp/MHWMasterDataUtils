using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MHWMasterDataUtils.Core
{
    public class Bow : WeaponBase
    {
        [JsonProperty("coatings")]
        public BowCoating[] Coatings { get; set; }
    }
}
