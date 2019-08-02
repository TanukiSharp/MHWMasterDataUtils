using System;
using System.Collections.Generic;
using MHWMasterDataUtils.Core;
using MHWMasterDataUtils.Equipment;
using MHWMasterDataUtils.Languages;
using MHWMasterDataUtils.Sharpness;
using MHWMasterDataUtils.Weapons;

namespace MHWMasterDataUtils.Builders.Weapons
{
    public class InsectGlaiveWeaponBuilder : MeleeWeaponBuilderBase<InsectGlaive>
    {
        public InsectGlaiveWeaponBuilder(
            LanguagePackageProcessor weaponsLanguages,
            WeaponsPackageProcessor weaponsPackageProcessor,
            EquipmentCraftPackageProcessor<WeaponType> craftPackageProcessor,
            EquipmentUpgradePackageProcessor equipmentUpgradePackageProcessor,
            SharpnessPackageProcessor sharpnessPackageProcessor
        )
            : base(
                  WeaponType.InsectGlaive,
                  weaponsLanguages,
                  weaponsPackageProcessor,
                  craftPackageProcessor,
                  equipmentUpgradePackageProcessor,
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
