using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MHWMasterDataUtils.Core
{
    public class JewelSkill
    {
        [JsonProperty("skillId")]
        public uint SkillId { get; set; }
        [JsonProperty("level")]
        public uint Level { get; set; }
    }

    public class Jewel : Item
    {
        [JsonProperty("equipmentId")]
        public uint EquipmentId { get; set; }
        [JsonProperty("skills")]
        public JewelSkill[] Skills { get; set; }
    }
}
