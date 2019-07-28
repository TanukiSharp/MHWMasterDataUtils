using System;
using System.Collections.Generic;
using System.Text;
using MHWMasterDataUtils.Crafting;
using MHWMasterDataUtils.Languages;
using MHWMasterDataUtils.Weapons;
using MHWMasterDataUtils.Weapons.Primitives;

using core = MHWMasterDataUtils.Core;

namespace MHWMasterDataUtils.Builders.Weapons
{
    public class BowWeaponBuilder : WeaponBuilderBase<RangeWeaponPrimitiveBase, core.Bow>
    {
        private readonly BowBottleTablePackageProcessor bowBottles;

        public BowWeaponBuilder(
            LanguagePackageProcessor weaponsLanguages,
            WeaponsPackageProcessor weaponsPackageProcessor,
            CraftPackageProcessor<core.WeaponType> craftPackageProcessor,
            WeaponUpgradePackageProcessor weaponUpgradePackageProcessor,
            BowBottleTablePackageProcessor bowBottles
        )
            : base(
                  core.WeaponType.Bow,
                  weaponsLanguages,
                  weaponsPackageProcessor,
                  craftPackageProcessor,
                  weaponUpgradePackageProcessor
            )
        {
            this.bowBottles = bowBottles;
        }

        protected override bool IsValidWeapon(RangeWeaponPrimitiveBase weapon)
        {
            return true;
        }

        protected override core.Bow CreateResultWeaponInstance()
        {
            return new core.Bow();
        }

        protected override void UpdateWeapon(RangeWeaponPrimitiveBase weapon, core.Bow resultWeapon)
        {
            BowBottleTableEntryPrimitive bowBottlesEntry = bowBottles.List[weapon.SpecialAmmoId];

            var coatings = new List<core.BowCoating>();

            if (bowBottlesEntry.CloseRange == 1)
                coatings.Add(core.BowCoating.CloseRange);
            else if (bowBottlesEntry.CloseRange == 2)
                coatings.Add(core.BowCoating.CloseRangePlus);

            if (bowBottlesEntry.Power == 1)
                coatings.Add(core.BowCoating.Power);
            else if (bowBottlesEntry.Power == 2)
                coatings.Add(core.BowCoating.PowerPlus);

            if (bowBottlesEntry.Paralysis == 1)
                coatings.Add(core.BowCoating.Paralysis);
            else if (bowBottlesEntry.Paralysis == 2)
                coatings.Add(core.BowCoating.ParalysisPlus);

            if (bowBottlesEntry.Poison == 1)
                coatings.Add(core.BowCoating.Poison);
            else if (bowBottlesEntry.Poison == 2)
                coatings.Add(core.BowCoating.PoisonPlus);

            if (bowBottlesEntry.Sleep == 1)
                coatings.Add(core.BowCoating.Sleep);
            else if (bowBottlesEntry.Sleep == 2)
                coatings.Add(core.BowCoating.SleepPlus);

            if (bowBottlesEntry.Blast == 1)
                coatings.Add(core.BowCoating.Blast);
            else if (bowBottlesEntry.Blast == 2)
                coatings.Add(core.BowCoating.BlastPlus);

            resultWeapon.Coatings = coatings.ToArray();
        }
    }
}
