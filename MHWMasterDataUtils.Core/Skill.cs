using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MHWMasterDataUtils.Core
{
    public class Ability
    {
        [JsonProperty("level")]
        public int Level { get; set; }
        [JsonProperty("name")]
        public Dictionary<string, string> Name { get; set; }
        [JsonProperty("description")]
        public Dictionary<string, string> Description { get; set; }
        [JsonProperty("params")]
        public int[] Parameters { get; set; }

        public override string ToString()
        {
            return $"{Name?[LanguageUtils.DefaultLanguageCode]} [{Level}] ({Description[LanguageUtils.DefaultLanguageCode]})";
        }
    }

    public class Skill
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("category")]
        public int Category { get; set; }
        [JsonProperty("isSetBonus")]
        public bool IsSetBonus { get; set; }
        [JsonProperty("name")]
        public Dictionary<string, string> Name { get; set; }
        [JsonProperty("reading")]
        public Dictionary<string, string> Reading { get; set; }
        [JsonProperty("description")]
        public Dictionary<string, string> Description { get; set; }
        [JsonProperty("abilities")]
        public Ability[] Abilities { get; set; }

        public override string ToString()
        {
            return $"{Id} {Name[LanguageUtils.DefaultLanguageCode]} ({Description[LanguageUtils.DefaultLanguageCode]})";
        }
    }
}
