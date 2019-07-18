using System;
using System.Collections.Generic;
using System.Text;

namespace MHWMasterDataUtils.Core
{
    public class SharpnessWeapon : WeaponBase
    {
        public SharpnessWeapon(
            WeaponType weaponType,
            uint id,
            Dictionary<string, string> name,
            Dictionary<string, string> description,
            ushort damage,
            byte rarity,
            byte treeId,
            SharpnessInfo currentSharpness,
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
            CurrentSharpness = currentSharpness;
            MaxSharpness = maxSharpness;
            WeaponSpecific = weaponSpecific;
        }

        public SharpnessInfo CurrentSharpness { get; }
        public SharpnessInfo MaxSharpness { get; }
        public object WeaponSpecific { get; }

        public override string ToString()
        {
            return Name[LanguageUtils.DefaultLanguageCode];
        }
    }
}
