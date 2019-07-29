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
    public abstract class MeleeWeaponBuilderBase<TResultWeapon> : WeaponBuilderBase<MeleeWeaponPrimitiveBase, TResultWeapon>
        where TResultWeapon : core.MeleeWeapon, new()
    {
        private readonly SharpnessPackageProcessor sharpnessPackageProcessor;

        protected MeleeWeaponBuilderBase(
            core.WeaponType weaponType,
            LanguagePackageProcessor weaponsLanguages,
            WeaponsPackageProcessor weaponsPackageProcessor,
            CraftPackageProcessor<core.WeaponType> craftPackageProcessor,
            WeaponUpgradePackageProcessor weaponUpgradePackageProcessor,
            SharpnessPackageProcessor sharpnessPackageProcessor
        )
            : base(
                  weaponType,
                  weaponsLanguages,
                  weaponsPackageProcessor,
                  craftPackageProcessor,
                  weaponUpgradePackageProcessor
            )
        {
            this.sharpnessPackageProcessor = sharpnessPackageProcessor;
        }

        protected override bool IsValidWeapon(MeleeWeaponPrimitiveBase weapon)
        {
            if (sharpnessPackageProcessor.Table.ContainsKey(weapon.SharpnessId) == false)
                return false;

            return true;
        }

        protected override void UpdateWeapon(MeleeWeaponPrimitiveBase weapon, TResultWeapon resultWeapon)
        {
            core.SharpnessInfo maxSharpness = sharpnessPackageProcessor.Table[weapon.SharpnessId];

            ushort sharpnessModifier = SharpnessUtils.ToSharpnessModifier(weapon.Handicraft);
            core.SharpnessInfo sharpness = SharpnessUtils.ApplySharpnessModifier(sharpnessModifier, maxSharpness);

            resultWeapon.Sharpness = sharpness;
            resultWeapon.MaxSharpness = maxSharpness;
        }
    }
}
