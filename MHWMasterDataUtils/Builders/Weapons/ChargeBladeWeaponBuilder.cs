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
    public class ChargeBladeWeaponBuilder : MeleeWeaponBuilderBase<ChargeBlade>
    {
        public ChargeBladeWeaponBuilder(
            LanguagePackageProcessor weaponsLanguages,
            WeaponsPackageProcessor weaponsPackageProcessor,
            CraftPackageProcessor<WeaponType> craftPackageProcessor,
            WeaponUpgradePackageProcessor weaponUpgradePackageProcessor,
            SharpnessPackageProcessor sharpnessPackageProcessor
        )
            : base(
                  WeaponType.ChargeBlade,
                  weaponsLanguages,
                  weaponsPackageProcessor,
                  craftPackageProcessor,
                  weaponUpgradePackageProcessor,
                  sharpnessPackageProcessor
            )
        {
        }

        protected override void UpdateWeapon(MeleeWeaponPrimitiveBase weapon, ChargeBlade resultWeapon)
        {
            base.UpdateWeapon(weapon, resultWeapon);

            resultWeapon.PhialType = (ChargeBladePhialType)weapon.Weapon1Id;
        }
    }
}
