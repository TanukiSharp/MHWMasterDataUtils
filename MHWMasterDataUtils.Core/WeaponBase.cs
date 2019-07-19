using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MHWMasterDataUtils.Core
{
    public abstract class WeaponBase
    {
        [JsonIgnore]
        public WeaponType WeaponType { get; }
        [JsonProperty("id")]
        public uint Id { get; }
        [JsonIgnore]
        public ushort TreeOrder { get; }
        [JsonProperty("parentId")]
        public int ParentId { get; }
        [JsonProperty("name")]
        public Dictionary<string, string> Name { get; }
        [JsonProperty("description")]
        public Dictionary<string, string> Description { get; }
        [JsonProperty("damage")]
        public ushort Damage { get; }
        [JsonProperty("rarity")]
        public byte Rarity { get; }
        [JsonProperty("treeId")]
        public byte TreeId { get; }
        [JsonProperty("affinity")]
        public sbyte Affinity { get; }
        [JsonProperty("craftingCost")]
        public uint CraftingCost { get; }
        [JsonProperty("defense")]
        public ushort Defense { get; }
        [JsonProperty("elderseal")]
        public Elderseal Elderseal { get; }
        [JsonProperty("elementStatus")]
        public ElementStatus ElementStatus { get; }
        [JsonProperty("elementStatusDamage")]
        public ushort ElementStatusDamage { get; }
        [JsonProperty("hiddenElementStatus")]
        public ElementStatus HiddenElementStatus { get; }
        [JsonProperty("hiddenElementStatusDamage")]
        public ushort HiddenElementStatusDamage { get; }
        [JsonProperty("slots")]
        public ushort[] Slots { get; }
        [JsonProperty("canDowngrade")]
        public bool CanDowngrade { get; }

        protected WeaponBase(
            WeaponType weaponType,
            uint id,
            ushort treeOrder,
            int parentId,
            Dictionary<string, string> name,
            Dictionary<string, string> description,
            ushort damage,
            byte rarity,
            byte treeId,
            sbyte affinity,
            uint craftingCost,
            ushort defense,
            Elderseal elderseal,
            ElementStatus elementStatus,
            ushort elementStatusDamage,
            ElementStatus hiddenElementStatus,
            ushort hiddenElementStatusDamage,
            ushort[] slots,
            bool canDowngrade
        )
        {
            WeaponType = weaponType;
            Id = id;
            TreeOrder = treeOrder;
            ParentId = parentId;
            Name = name;
            Description = description;
            Damage = damage;
            Rarity = rarity;
            TreeId = treeId;
            Affinity = affinity;
            CraftingCost = craftingCost;
            Defense = defense;
            Elderseal = elderseal;
            ElementStatus = elementStatus;
            ElementStatusDamage = elementStatusDamage;
            HiddenElementStatus = hiddenElementStatus;
            HiddenElementStatusDamage = hiddenElementStatusDamage;
            Slots = slots;
            CanDowngrade = canDowngrade;
        }
    }
}
