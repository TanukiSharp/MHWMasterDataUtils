using System;
using System.Collections.Generic;
using MHWMasterDataUtils.Core;
using MHWMasterDataUtils.Crafting;
using MHWMasterDataUtils.Languages;
using MHWMasterDataUtils.Sharpness;
using MHWMasterDataUtils.Weapons;
using MHWMasterDataUtils.Weapons.Primitives;

namespace MHWMasterDataUtils.Builders.Weapons
{
    public class InsectGlaiveWeaponBuilder : MeleeWeaponBuilderBase<InsectGlaive>
    {
        public InsectGlaiveWeaponBuilder(
            LanguagePackageProcessor weaponsLanguages,
            WeaponsPackageProcessor weaponsPackageProcessor,
            CraftPackageProcessor<WeaponType> craftPackageProcessor,
            WeaponUpgradePackageProcessor weaponUpgradePackageProcessor,
            SharpnessPackageProcessor sharpnessPackageProcessor
        )
            : base(
                  WeaponType.InsectGlaive,
                  weaponsLanguages,
                  weaponsPackageProcessor,
                  craftPackageProcessor,
                  weaponUpgradePackageProcessor,
                  sharpnessPackageProcessor
            )
        {
        }

        protected override void UpdateWeapon(MeleeWeaponPrimitiveBase weapon, InsectGlaive resultWeapon)
        {
            base.UpdateWeapon(weapon, resultWeapon);

            resultWeapon.InsectBonus = weapon.Weapon1Id; // Matches type core.KinsectBonus.
        }
    }
}
