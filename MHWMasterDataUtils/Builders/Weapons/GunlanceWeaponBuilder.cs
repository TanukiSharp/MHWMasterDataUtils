﻿using System;
using System.Collections.Generic;
using MHWMasterDataUtils.Core;
using MHWMasterDataUtils.Crafting;
using MHWMasterDataUtils.Languages;
using MHWMasterDataUtils.Sharpness;
using MHWMasterDataUtils.Weapons;
using MHWMasterDataUtils.Weapons.Primitives;

namespace MHWMasterDataUtils.Builders.Weapons
{
    public class GunlanceWeaponBuilder : MeleeWeaponBuilderBase<Gunlance>
    {
        private readonly GunlanceShellPackageProcessor gunlanceShells;

        public GunlanceWeaponBuilder(
            LanguagePackageProcessor weaponsLanguages,
            WeaponsPackageProcessor weaponsPackageProcessor,
            CraftPackageProcessor<WeaponType> craftPackageProcessor,
            WeaponUpgradePackageProcessor weaponUpgradePackageProcessor,
            SharpnessPackageProcessor sharpnessPackageProcessor,
            GunlanceShellPackageProcessor gunlanceShells
        )
            : base(
                  WeaponType.Gunlance,
                  weaponsLanguages,
                  weaponsPackageProcessor,
                  craftPackageProcessor,
                  weaponUpgradePackageProcessor,
                  sharpnessPackageProcessor
            )
        {
            this.gunlanceShells = gunlanceShells;
        }

        protected override void UpdateWeapon(MeleeWeaponPrimitiveBase weapon, Gunlance resultWeapon)
        {
            base.UpdateWeapon(weapon, resultWeapon);

            GunlanceShellPrimitive gunlanceShell = gunlanceShells.Table[weapon.Weapon1Id];

            resultWeapon.Shelling = new GunlanceShell
            {
                ShellType = (int)gunlanceShell.ShellType, // Matches type core.GunlanceShellType.
                ShellLevel = gunlanceShell.ShellLevel + 1
            };
        }
    }
}
