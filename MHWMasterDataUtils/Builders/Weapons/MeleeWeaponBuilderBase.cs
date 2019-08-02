using System;
using System.Collections.Generic;
using MHWMasterDataUtils.Equipment;
using MHWMasterDataUtils.Languages;
using MHWMasterDataUtils.Sharpness;
using MHWMasterDataUtils.Weapons;

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
            EquipmentCraftPackageProcessor<core.WeaponType> craftPackageProcessor,
            EquipmentUpgradePackageProcessor equipmentUpgradePackageProcessor,
            SharpnessPackageProcessor sharpnessPackageProcessor
        )
            : base(
                  weaponType,
                  weaponsLanguages,
                  weaponsPackageProcessor,
                  craftPackageProcessor,
                  equipmentUpgradePackageProcessor
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
