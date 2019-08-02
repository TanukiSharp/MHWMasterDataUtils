using System;
using System.Collections.Generic;
using MHWMasterDataUtils.Core;
using MHWMasterDataUtils.Equipment;
using MHWMasterDataUtils.Languages;
using MHWMasterDataUtils.Sharpness;
using MHWMasterDataUtils.Weapons;

namespace MHWMasterDataUtils.Builders.Weapons
{
    public class ChargeBladeWeaponBuilder : MeleeWeaponBuilderBase<ChargeBlade>
    {
        public ChargeBladeWeaponBuilder(
            LanguagePackageProcessor weaponsLanguages,
            WeaponsPackageProcessor weaponsPackageProcessor,
            EquipmentCraftPackageProcessor<WeaponType> craftPackageProcessor,
            EquipmentUpgradePackageProcessor equipmentUpgradePackageProcessor,
            SharpnessPackageProcessor sharpnessPackageProcessor
        )
            : base(
                  WeaponType.ChargeBlade,
                  weaponsLanguages,
                  weaponsPackageProcessor,
                  craftPackageProcessor,
                  equipmentUpgradePackageProcessor,
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
