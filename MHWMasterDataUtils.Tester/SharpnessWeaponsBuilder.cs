using System;
using System.Collections.Generic;
using System.Text;
using MHWMasterDataUtils.Crafting;
using MHWMasterDataUtils.Equipments;
using MHWMasterDataUtils.Items;
using MHWMasterDataUtils.Languages;
using MHWMasterDataUtils.Sharpness;
using MHWMasterDataUtils.Weapons;
using MHWMasterDataUtils.Weapons.Primitives;

namespace MHWMasterDataUtils.Tester
{
    public class SharpnessWeaponsBuilder
    {
        private readonly WeaponClass weaponClass;
        private readonly WeaponUpgradePackageProcessor weaponUpgrade;
        private readonly CraftPackageProcessor creationCraft;
        private readonly WeaponUpgradePackageProcessor upgradeCraft;
        private readonly ItemsPackageProcessor items;
        private readonly SharpnessPackageProcessor sharpness;
        private readonly LanguagePackageProcessor weaponLanguage;
        private readonly LanguagePackageProcessor weaponSeriesLanguages;
        private readonly WeaponsPackageProcessor weapons;

        public SharpnessWeaponsBuilder(
            WeaponClass weaponClass,
            WeaponUpgradePackageProcessor weaponUpgrade,
            CraftPackageProcessor creationCraft,
            WeaponUpgradePackageProcessor upgradeCraft,
            ItemsPackageProcessor items,
            SharpnessPackageProcessor sharpness,
            LanguagePackageProcessor weaponLanguage,
            LanguagePackageProcessor weaponSeriesLanguages,
            WeaponsPackageProcessor weapons
        )
        {
            this.weaponClass = weaponClass;
            this.weaponUpgrade = weaponUpgrade;
            this.creationCraft = creationCraft;
            this.upgradeCraft = upgradeCraft;
            this.items = items;
            this.sharpness = sharpness;
            this.weaponLanguage = weaponLanguage;
            this.weaponSeriesLanguages = weaponSeriesLanguages;
            this.weapons = weapons;
        }

        public object Build()
        {
            foreach (KeyValuePair<uint, WeaponPrimitiveBase> weapon in weapons.Table[weaponClass])
            {
                if (weaponLanguage.Table[LanguageIdPrimitive.English].TryGetValue(weapon.Value.GmdNameIndex, out LanguageItem weaponLanguageItem) == false)
                    continue;

                if (weaponLanguageItem.Value == string.Empty || weaponLanguageItem.Value == "Invalid Message")
                    continue;

                if (weaponSeriesLanguages.Table[LanguageIdPrimitive.English].TryGetValue(weapon.Value.TreeId, out LanguageItem treeSeriesLanguageItem) == false)
                    continue;

                string weaponName = weaponLanguageItem.Value;
                string treeName = treeSeriesLanguageItem.Value;

            }


            return null;
        }
    }
}
