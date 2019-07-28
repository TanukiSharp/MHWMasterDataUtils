using System;
using System.Collections.Generic;
using MHWMasterDataUtils.Crafting;
using MHWMasterDataUtils.Languages;
using MHWMasterDataUtils.Weapons;
using MHWMasterDataUtils.Weapons.Primitives;

using core = MHWMasterDataUtils.Core;

namespace MHWMasterDataUtils.Builders.Weapons
{
    public class BowgunWeaponBuilder : WeaponBuilderBase<RangeWeaponPrimitiveBase, core.Bowgun>
    {
        protected readonly AmmoPackageProcessor ammos;

        public BowgunWeaponBuilder(
            core.WeaponType weaponType,
            LanguagePackageProcessor weaponsLanguages,
            WeaponsPackageProcessor weaponsPackageProcessor,
            CraftPackageProcessor<core.WeaponType> craftPackageProcessor,
            WeaponUpgradePackageProcessor weaponUpgradePackageProcessor,
            AmmoPackageProcessor ammos
        )
            : base(
                  weaponType,
                  weaponsLanguages,
                  weaponsPackageProcessor,
                  craftPackageProcessor,
                  weaponUpgradePackageProcessor
            )
        {
            if (weaponType != core.WeaponType.LightBowgun && weaponType != core.WeaponType.HeavyBowgun)
                throw new ArgumentException($"Invalid '{nameof(weaponType)}' argument. Expected '{core.WeaponType.LightBowgun}' or '{core.WeaponType.HeavyBowgun}' but got '{weaponType}'.");
            this.ammos = ammos;
        }

        protected override bool IsValidWeapon(RangeWeaponPrimitiveBase weapon)
        {
            return true;
        }

        protected override core.Bowgun CreateResultWeaponInstance()
        {
            return new core.Bowgun();
        }

        private void AddAvailableAmmo(List<core.Ammo> availableAmmos, core.AmmoType ammoType, AmmoEntryPrimitive ammoEntry)
        {
            if (ammoEntry.Capacity == 0)
                return;

            availableAmmos.Add(new core.Ammo
            {
                Type = ammoType,
                Capacity = ammoEntry.Capacity,
                ShotType = ammoEntry.ShotType,
                Reload = ammoEntry.Reload
            });
        }

        protected override void UpdateWeapon(RangeWeaponPrimitiveBase weapon, core.Bowgun resultWeapon)
        {
            resultWeapon.SpecialAmmo = (core.BowgunSpecialAmmo)weapon.SpecialAmmoId;
            resultWeapon.Deviation = weapon.Deviation;

            AmmoTableEntryPrimitive ammoTableEntry = ammos.List[weapon.ShellTableId];

            var availableAmmos = new List<core.Ammo>();

            AddAvailableAmmo(availableAmmos, core.AmmoType.Normal1, ammoTableEntry.Normal1);
            AddAvailableAmmo(availableAmmos, core.AmmoType.Normal2, ammoTableEntry.Normal2);
            AddAvailableAmmo(availableAmmos, core.AmmoType.Normal3, ammoTableEntry.Normal3);
            AddAvailableAmmo(availableAmmos, core.AmmoType.Pierce1, ammoTableEntry.Pierce1);
            AddAvailableAmmo(availableAmmos, core.AmmoType.Pierce2, ammoTableEntry.Pierce2);
            AddAvailableAmmo(availableAmmos, core.AmmoType.Pierce3, ammoTableEntry.Pierce3);
            AddAvailableAmmo(availableAmmos, core.AmmoType.Spread1, ammoTableEntry.Spread1);
            AddAvailableAmmo(availableAmmos, core.AmmoType.Spread2, ammoTableEntry.Spread2);
            AddAvailableAmmo(availableAmmos, core.AmmoType.Spread3, ammoTableEntry.Spread3);
            AddAvailableAmmo(availableAmmos, core.AmmoType.Cluster1, ammoTableEntry.Cluster1);
            AddAvailableAmmo(availableAmmos, core.AmmoType.Cluster2, ammoTableEntry.Cluster2);
            AddAvailableAmmo(availableAmmos, core.AmmoType.Cluster3, ammoTableEntry.Cluster3);
            AddAvailableAmmo(availableAmmos, core.AmmoType.Wyvern, ammoTableEntry.Wyvern);
            AddAvailableAmmo(availableAmmos, core.AmmoType.Sticky1, ammoTableEntry.Sticky1);
            AddAvailableAmmo(availableAmmos, core.AmmoType.Sticky2, ammoTableEntry.Sticky2);
            AddAvailableAmmo(availableAmmos, core.AmmoType.Sticky3, ammoTableEntry.Sticky3);
            AddAvailableAmmo(availableAmmos, core.AmmoType.Slicing, ammoTableEntry.Slicing);
            AddAvailableAmmo(availableAmmos, core.AmmoType.Flaming, ammoTableEntry.Flaming);
            AddAvailableAmmo(availableAmmos, core.AmmoType.Water, ammoTableEntry.Water);
            AddAvailableAmmo(availableAmmos, core.AmmoType.Freeze, ammoTableEntry.Freeze);
            AddAvailableAmmo(availableAmmos, core.AmmoType.Thunder, ammoTableEntry.Thunder);
            AddAvailableAmmo(availableAmmos, core.AmmoType.Dragon, ammoTableEntry.Dragon);
            AddAvailableAmmo(availableAmmos, core.AmmoType.Poison1, ammoTableEntry.Poison1);
            AddAvailableAmmo(availableAmmos, core.AmmoType.Poison2, ammoTableEntry.Poison2);
            AddAvailableAmmo(availableAmmos, core.AmmoType.Paralysis1, ammoTableEntry.Paralysis1);
            AddAvailableAmmo(availableAmmos, core.AmmoType.Paralysis2, ammoTableEntry.Paralysis2);
            AddAvailableAmmo(availableAmmos, core.AmmoType.Sleep1, ammoTableEntry.Sleep1);
            AddAvailableAmmo(availableAmmos, core.AmmoType.Sleep2, ammoTableEntry.Sleep2);
            AddAvailableAmmo(availableAmmos, core.AmmoType.Exhaust1, ammoTableEntry.Exhaust1);
            AddAvailableAmmo(availableAmmos, core.AmmoType.Exhaust2, ammoTableEntry.Exhaust2);
            AddAvailableAmmo(availableAmmos, core.AmmoType.Recover1, ammoTableEntry.Recover1);
            AddAvailableAmmo(availableAmmos, core.AmmoType.Recover2, ammoTableEntry.Recover2);
            AddAvailableAmmo(availableAmmos, core.AmmoType.Demon, ammoTableEntry.Demon);
            AddAvailableAmmo(availableAmmos, core.AmmoType.Armor, ammoTableEntry.Armor);
            AddAvailableAmmo(availableAmmos, core.AmmoType.Tranq, ammoTableEntry.Tranq);

            resultWeapon.Ammos = availableAmmos.ToArray();
        }
    }
}
