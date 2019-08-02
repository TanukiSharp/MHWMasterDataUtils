using System;
using System.Collections.Generic;
using MHWMasterDataUtils.Core;
using MHWMasterDataUtils.Equipment;
using MHWMasterDataUtils.Languages;
using MHWMasterDataUtils.Sharpness;
using MHWMasterDataUtils.Weapons;

namespace MHWMasterDataUtils.Builders.Weapons
{
    public class DualBladesWeaponBuilder : MeleeWeaponBuilderBase<DualBlades>
    {
        private readonly DualBladesSpecialPackageProcessor dualBladesSpecial;

        public DualBladesWeaponBuilder(
            LanguagePackageProcessor weaponsLanguages,
            WeaponsPackageProcessor weaponsPackageProcessor,
            EquipmentCraftPackageProcessor<WeaponType> craftPackageProcessor,
            EquipmentUpgradePackageProcessor equipmentUpgradePackageProcessor,
            SharpnessPackageProcessor sharpnessPackageProcessor,
            DualBladesSpecialPackageProcessor dualBladesSpecial
        )
            : base(
                  WeaponType.DualBlades,
                  weaponsLanguages,
                  weaponsPackageProcessor,
                  craftPackageProcessor,
                  equipmentUpgradePackageProcessor,
                  sharpnessPackageProcessor
            )
        {
            this.dualBladesSpecial = dualBladesSpecial;
        }

        protected override void UpdateWeapon(MeleeWeaponPrimitiveBase weapon, DualBlades resultWeapon)
        {
            base.UpdateWeapon(weapon, resultWeapon);

            if (weapon.Weapon1Id == 0)
                return;

            DualBladesSpecialPrimitive dualBladesElementInfo = dualBladesSpecial.Table[weapon.Weapon1Id];

            resultWeapon.ElementStatus = dualBladesElementInfo.Element1;
            resultWeapon.ElementStatusDamage = dualBladesElementInfo.Element1Damage * 10;
            resultWeapon.SecondaryElementStatus = dualBladesElementInfo.Element2;
            resultWeapon.SecondaryElementStatusDamage = dualBladesElementInfo.Element2Damage * 10;
        }
    }
}
