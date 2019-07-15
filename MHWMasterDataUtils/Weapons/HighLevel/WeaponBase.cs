using System;
using System.Collections.Generic;
using System.Text;
using MHWMasterDataUtils.Equipments;
using Newtonsoft.Json;

namespace MHWMasterDataUtils.Weapons.HighLevel
{
    public abstract class WeaponBase
    {
        [JsonIgnore]
        public WeaponType WeaponType { get; }
        public uint Id { get; }
        public Dictionary<string, string> Name { get; }
        public Dictionary<string, string> Description { get; }
        public ushort Damage { get; }
        public byte Rarity { get; }
        public byte TreeId { get; }
        public sbyte Affinity { get; }
        public uint CraftingCost { get; }
        public ushort Defense { get; }
        public Elderseal Elderseal { get; }
        public ElementStatus ElementStatus { get; }
        public ushort ElementStatusDamage { get; }
        public ElementStatus HiddenElementStatus { get; }
        public ushort HiddenElementStatusDamage { get; }
        public ushort[] Slots { get; }
        public bool CanDowngrade { get; }

        protected WeaponBase(
            WeaponType weaponType,
            uint id,
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
