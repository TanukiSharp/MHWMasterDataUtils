using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MHWMasterDataUtils.Core
{
    public class SharpnessWeapon : WeaponBase
    {
        public SharpnessWeapon(
            WeaponType weaponType,
            uint id,
            ushort treeOrder,
            int parentId,
            Dictionary<string, string> name,
            Dictionary<string, string> description,
            ushort damage,
            byte rarity,
            byte treeId,
            SharpnessInfo sharpness,
            SharpnessInfo maxSharpness,
            sbyte affinity,
            uint craftingCost,
            ushort defense,
            Elderseal elderseal,
            ElementStatus elementStatus,
            ushort elementStatusDamange,
            ElementStatus hiddenElementStatus,
            ushort hiddenElementStatusDamange,
            ushort[] slots,
            bool canDowngrade,
            object weaponSpecific
        )
            : base(
                  weaponType,
                  id,
                  treeOrder,
                  parentId,
                  name,
                  description,
                  damage,
                  rarity,
                  treeId,
                  affinity,
                  craftingCost,
                  defense,
                  elderseal,
                  elementStatus,
                  elementStatusDamange,
                  hiddenElementStatus,
                  hiddenElementStatusDamange,
                  slots,
                  canDowngrade
            )
        {
            Sharpness = sharpness;
            MaxSharpness = maxSharpness;
            WeaponSpecific = weaponSpecific;
        }

        [JsonProperty("sharpness")]
        public SharpnessInfo Sharpness { get; }
        [JsonProperty("maxSharpness")]
        public SharpnessInfo MaxSharpness { get; }
        [JsonProperty("weaponSpecific")]
        public object WeaponSpecific { get; }

        public override string ToString()
        {
            return $"[{Id}] {Name[LanguageUtils.DefaultLanguageCode]} (parent: {ParentId})";
        }
    }
}
