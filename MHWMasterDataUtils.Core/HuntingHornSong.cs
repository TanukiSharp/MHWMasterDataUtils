using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MHWMasterDataUtils.Core
{
    public class HuntingHornSong
    {
        [JsonProperty("effect")]
        public string Effect { get; set; }
        [JsonProperty("note1")]
        public string Note1 { get; set; }
        [JsonProperty("note2")]
        public string Note2 { get; set; }
        [JsonProperty("note3")]
        public string Note3 { get; set; }
        [JsonProperty("note4")]
        public string Note4 { get; set; }
    }
}
