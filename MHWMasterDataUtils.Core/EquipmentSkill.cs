using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MHWMasterDataUtils.Core
{
    public class EquipmentSkill
    {
        [JsonProperty("skillId")]
        public int SkillId { get; set; }
        [JsonProperty("level")]
        public int? Level { get; set; }
        [JsonProperty("requiredParts")]
        public int? RequiredParts { get; set; }
    }
}
