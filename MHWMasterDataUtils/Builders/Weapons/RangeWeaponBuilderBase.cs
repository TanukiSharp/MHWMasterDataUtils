using System;
using System.Collections.Generic;
using MHWMasterDataUtils.Crafting;
using MHWMasterDataUtils.Languages;
using MHWMasterDataUtils.Sharpness;
using MHWMasterDataUtils.Weapons;
using MHWMasterDataUtils.Weapons.Primitives;

using core = MHWMasterDataUtils.Core;

namespace MHWMasterDataUtils.Builders.Weapons
{
    public abstract class RangeWeaponBuilderBase<TResultWeapon> : WeaponBuilderBase<RangeWeaponPrimitiveBase, TResultWeapon>
        where TResultWeapon : core.WeaponBase, new()
    {
        protected RangeWeaponBuilderBase(
            core.WeaponType weaponType,
            LanguagePackageProcessor weaponsLanguages,
            WeaponsPackageProcessor weaponsPackageProcessor,
            CraftPackageProcessor<core.WeaponType> craftPackageProcessor,
            WeaponUpgradePackageProcessor weaponUpgradePackageProcessor
        )
            : base(
                  weaponType,
                  weaponsLanguages,
                  weaponsPackageProcessor,
                  craftPackageProcessor,
                  weaponUpgradePackageProcessor
            )
        {
        }

        protected override bool IsValidWeapon(RangeWeaponPrimitiveBase weapon)
        {
            return true;
        }

        protected override TResultWeapon CreateResultWeaponInstance()
        {
            return new TResultWeapon();
        }
    }
}
