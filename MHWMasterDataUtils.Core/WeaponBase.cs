using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MHWMasterDataUtils.Core
{
    public abstract class WeaponBase
    {
        [JsonIgnore]
        public int TreeOrder { get; set; }

        [JsonProperty("id")]
        public uint Id { get; set; }
        [JsonProperty("parentId")]
        public int ParentId { get; set; }
        [JsonProperty("sortOrder")]
        public int SortOrder { get; set; }
        [JsonProperty("name")]
        public Dictionary<string, string> Name { get; set; }
        [JsonProperty("description")]
        public Dictionary<string, string> Description { get; set; }
        [JsonProperty("damage")]
        public int Damage { get; set; }
        [JsonProperty("rarity")]
        public int Rarity { get; set; }
        [JsonProperty("treeId")]
        public int TreeId { get; set; }
        [JsonProperty("affinity")]
        public int Affinity { get; set; }
        [JsonProperty("craftingCost")]
        public uint CraftingCost { get; set; }
        [JsonProperty("defense")]
        public int Defense { get; set; }
        [JsonProperty("elderseal")]
        public Elderseal Elderseal { get; set; }
        [JsonProperty("elementStatus")]
        public ElementStatus ElementStatus { get; set; }
        [JsonProperty("elementStatusDamage")]
        public int ElementStatusDamage { get; set; }
        [JsonProperty("hiddenElementStatus")]
        public ElementStatus HiddenElementStatus { get; set; }
        [JsonProperty("hiddenElementStatusDamage")]
        public int HiddenElementStatusDamage { get; set; }
        [JsonProperty("skillId")]
        public int? SkillId { get; set; }
        [JsonProperty("slots")]
        public int[] Slots { get; set; }
        [JsonProperty("canDowngrade")]
        public bool CanDowngrade { get; set; }
        [JsonProperty("craft")]
        public Craft Craft { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
