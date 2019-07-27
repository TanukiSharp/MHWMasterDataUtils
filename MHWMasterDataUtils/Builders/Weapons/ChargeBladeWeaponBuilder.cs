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
        private readonly AxePhialPackageProcessor axePhials;

        public ChargeBladeWeaponBuilder(
            LanguagePackageProcessor weaponsLanguages,
            WeaponsPackageProcessor weaponsPackageProcessor,
            CraftPackageProcessor<WeaponType> craftPackageProcessor,
            WeaponUpgradePackageProcessor weaponUpgradePackageProcessor,
            SharpnessPackageProcessor sharpnessPackageProcessor,
            AxePhialPackageProcessor axePhials
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
            this.axePhials = axePhials;
        }

        protected override ChargeBlade CreateResultWeaponInstance()
        {
            return new ChargeBlade();
        }

        protected override void UpdateWeapon(MeleeWeaponPrimitiveBase weapon, ChargeBlade resultWeapon)
        {
            base.UpdateWeapon(weapon, resultWeapon);

            AxePhialPrimitive axePhial = axePhials.Table[weapon.Weapon1Id];

            resultWeapon.PhialType = (ChargeBladePhialType)axePhial.PhialType;
        }
    }
}
