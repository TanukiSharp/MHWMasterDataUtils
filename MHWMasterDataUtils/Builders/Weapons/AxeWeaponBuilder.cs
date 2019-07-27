using System;
using System.Collections.Generic;
using System.Text;
using MHWMasterDataUtils.Core;
using MHWMasterDataUtils.Crafting;
using MHWMasterDataUtils.Languages;
using MHWMasterDataUtils.Sharpness;
using MHWMasterDataUtils.Weapons;
using MHWMasterDataUtils.Weapons.Primitives;

namespace MHWMasterDataUtils.Builders.Weapons
{
    public class AxeWeaponBuilder : SharpnessWeaponBuilderBase
    {
        private readonly AxePhialPackageProcessor axePhials;

        public AxeWeaponBuilder(
            WeaponType weaponType,
            LanguagePackageProcessor weaponsLanguages,
            WeaponsPackageProcessor weaponsPackageProcessor,
            CraftPackageProcessor<WeaponType> craftPackageProcessor,
            WeaponUpgradePackageProcessor weaponUpgradePackageProcessor,
            SharpnessPackageProcessor sharpnessPackageProcessor,
            AxePhialPackageProcessor axePhials
        )
            : base(
                  weaponType,
                  weaponsLanguages,
                  weaponsPackageProcessor,
                  craftPackageProcessor,
                  weaponUpgradePackageProcessor,
                  sharpnessPackageProcessor
            )
        {
            if (weaponType != WeaponType.SwitchAxe && weaponType != WeaponType.ChargeBlade)
                throw new ArgumentException($"Invalide '{nameof(weaponType)} argument. Expected '{WeaponType.SwitchAxe}' or '{WeaponType.ChargeBlade}' but got '{weaponType}'.");
            this.axePhials = axePhials;
        }

        protected override object CreateWeaponSpecificValue(MeleeWeaponPrimitiveBase weapon)
        {
            AxePhialPrimitive axePhial = axePhials.Table[weapon.Weapon1Id];

            return new AxePhial
            {
                ElementStatus = (int)axePhial.ElementStatus,
                Damage = axePhial.Damage * 10
            };
        }
    }
}
