using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MHWMasterDataUtils.Core
{
    public class HuntingHornSong
    {
        [JsonProperty("effect")]
        public int Effect { get; set; }
        [JsonProperty("note1")]
        public int? Note1 { get; set; }
        [JsonProperty("note2")]
        public int? Note2 { get; set; }
        [JsonProperty("note3")]
        public int? Note3 { get; set; }
        [JsonProperty("note4")]
        public int? Note4 { get; set; }
    }
}
