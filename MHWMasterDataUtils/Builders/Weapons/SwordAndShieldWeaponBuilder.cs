using System;
using System.Collections.Generic;
using MHWMasterDataUtils.Core;
using MHWMasterDataUtils.Equipment;
using MHWMasterDataUtils.Languages;
using MHWMasterDataUtils.Sharpness;
using MHWMasterDataUtils.Weapons;

namespace MHWMasterDataUtils.Builders.Weapons
{
    public class SwordAndShieldWeaponBuilder : MeleeWeaponBuilderBase<MeleeWeapon>
    {
        public SwordAndShieldWeaponBuilder(
            LanguagePackageProcessor weaponsLanguages,
            WeaponsPackageProcessor weaponsPackageProcessor,
            EquipmentCraftPackageProcessor<WeaponType> craftPackageProcessor,
            EquipmentUpgradePackageProcessor equipmentUpgradePackageProcessor,
            SharpnessPackageProcessor sharpnessPackageProcessor
        )
            : base(
                  WeaponType.SwordAndShield,
                  weaponsLanguages,
                  weaponsPackageProcessor,
                  craftPackageProcessor,
                  equipmentUpgradePackageProcessor,
                  sharpnessPackageProcessor
            )
        {
        }
    }
}
