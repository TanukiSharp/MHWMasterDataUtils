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
    public class DualBladesWeaponBuilder : SharpnessWeaponBuilderBase
    {
        private readonly DualBladesSpecialPackageProcessor dualBladesSpecial;

        public DualBladesWeaponBuilder(
            LanguagePackageProcessor weaponsLanguages,
            WeaponsPackageProcessor weaponsPackageProcessor,
            CraftPackageProcessor<WeaponType> craftPackageProcessor,
            WeaponUpgradePackageProcessor weaponUpgradePackageProcessor,
            SharpnessPackageProcessor sharpnessPackageProcessor,
            DualBladesSpecialPackageProcessor dualBladesSpecial
        )
            : base(
                  WeaponType.DualBlades,
                  weaponsLanguages,
                  weaponsPackageProcessor,
                  craftPackageProcessor,
                  weaponUpgradePackageProcessor,
                  sharpnessPackageProcessor
            )
        {
            this.dualBladesSpecial = dualBladesSpecial;
        }

        protected override object CreateWeaponSpecificValue(MeleeWeaponPrimitiveBase weapon)
        {
            if (weapon.Weapon1Id == 0)
                return null;

            DualBladesSpecialPrimitive dualBladesElementInfo = dualBladesSpecial.Table[weapon.Weapon1Id];

            return new
            {
                elementStatus1 = (int)dualBladesElementInfo.Element1, // Matches type core.ElementStatus.
                elementStatus1Damage = dualBladesElementInfo.Element1Damage * 10,
                elementStatus2 = (int)dualBladesElementInfo.Element2, // Matches type core.ElementStatus.
                elementStatus2Damage = dualBladesElementInfo.Element2Damage * 10,
            };
        }
    }
}
