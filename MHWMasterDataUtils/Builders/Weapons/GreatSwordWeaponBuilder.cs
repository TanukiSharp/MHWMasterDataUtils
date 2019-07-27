using System;
using System.Collections.Generic;
using System.Text;
using MHWMasterDataUtils.Core;
using MHWMasterDataUtils.Crafting;
using MHWMasterDataUtils.Languages;
using MHWMasterDataUtils.Sharpness;
using MHWMasterDataUtils.Weapons;

namespace MHWMasterDataUtils.Builders.Weapons
{
    public class GreatSwordWeaponBuilder : SharpnessWeaponBuilderBase
    {
        public GreatSwordWeaponBuilder(
            LanguagePackageProcessor weaponsLanguages,
            WeaponsPackageProcessor weaponsPackageProcessor,
            CraftPackageProcessor<WeaponType> craftPackageProcessor,
            WeaponUpgradePackageProcessor weaponUpgradePackageProcessor,
            SharpnessPackageProcessor sharpnessPackageProcessor
        )
            : base(
                  WeaponType.GreatSword,
                  weaponsLanguages,
                  weaponsPackageProcessor,
                  craftPackageProcessor,
                  weaponUpgradePackageProcessor,
                  sharpnessPackageProcessor
            )
        {
        }
    }
}
