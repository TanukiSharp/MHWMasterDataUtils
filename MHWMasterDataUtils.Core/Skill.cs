using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MHWMasterDataUtils.Core
{
    public class Ability
    {
        [JsonProperty("id")]
        public uint Id { get; set; }
        [JsonProperty("level")]
        public int? Level { get; set; }
        [JsonProperty("requiredParts")]
        public int? RequiredParts { get; set; }
        [JsonProperty("skillId")]
        public int? SkillId { get; set; }
        [JsonProperty("name")]
        public Dictionary<string, string> Name { get; set; }
        [JsonProperty("description")]
        public Dictionary<string, string> Description { get; set; }
        [JsonProperty("params")]
        public IReadOnlyCollection<short> Parameters { get; set; }

        public static Ability CreateRegularSkill(uint abilityId, int level, Dictionary<string, string> description, IReadOnlyCollection<short> parameters)
        {
            return new Ability
            {
                Id = abilityId,
                Level = level,
                RequiredParts = null,
                Name = null,
                Description = description,
                Parameters = parameters
            };
        }

        public static Ability CreateSetSkill(uint abilityId, int requiredParts, Dictionary<string, string> name, Dictionary<string, string> description, IReadOnlyCollection<short> parameters)
        {
            return new Ability
            {
                Id = abilityId,
                Level = null,
                RequiredParts = requiredParts,
                Name = name,
                Description = description,
                Parameters = parameters
            };
        }

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
            string description = Description?[LanguageUtils.DefaultLanguageCode];
            return $"{Id} {Name[LanguageUtils.DefaultLanguageCode]}{(description != null ? $" ({description})": string.Empty)}";
        }
    }
}
