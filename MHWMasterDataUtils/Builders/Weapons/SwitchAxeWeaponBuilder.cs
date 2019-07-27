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
    public class SwitchAxeWeaponBuilder : MeleeWeaponBuilderBase<SwitchAxe>
    {
        private readonly AxePhialPackageProcessor axePhials;

        public SwitchAxeWeaponBuilder(
            LanguagePackageProcessor weaponsLanguages,
            WeaponsPackageProcessor weaponsPackageProcessor,
            CraftPackageProcessor<WeaponType> craftPackageProcessor,
            WeaponUpgradePackageProcessor weaponUpgradePackageProcessor,
            SharpnessPackageProcessor sharpnessPackageProcessor,
            AxePhialPackageProcessor axePhials
        )
            : base(
                  WeaponType.SwitchAxe,
                  weaponsLanguages,
                  weaponsPackageProcessor,
                  craftPackageProcessor,
                  weaponUpgradePackageProcessor,
                  sharpnessPackageProcessor
            )
        {
            this.axePhials = axePhials;
        }

        protected override SwitchAxe CreateResultWeaponInstance()
        {
            return new SwitchAxe();
        }

        protected override void UpdateWeapon(MeleeWeaponPrimitiveBase weapon, SwitchAxe resultWeapon)
        {
            base.UpdateWeapon(weapon, resultWeapon);

            AxePhialPrimitive axePhial = axePhials.Table[weapon.Weapon1Id];

            int? damage = null;
            if (axePhial.Damage > 0)
                damage = axePhial.Damage * 10;

            resultWeapon.Phial = new SwitchAxePhial
            {
                Type = (SwitchAxePhialType)axePhial.PhialType,
                Damage = damage
            };
        }
    }
}
