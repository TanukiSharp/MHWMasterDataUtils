using System;
using System.Collections.Generic;
using MHWMasterDataUtils.Core;
using MHWMasterDataUtils.Equipment;
using MHWMasterDataUtils.Languages;
using MHWMasterDataUtils.Sharpness;
using MHWMasterDataUtils.Weapons;

namespace MHWMasterDataUtils.Builders.Weapons
{
    public class SwitchAxeWeaponBuilder : MeleeWeaponBuilderBase<SwitchAxe>
    {
        private readonly SwitchAxePhialPackageProcessor axePhials;

        public SwitchAxeWeaponBuilder(
            LanguagePackageProcessor weaponsLanguages,
            WeaponsPackageProcessor weaponsPackageProcessor,
            EquipmentCraftPackageProcessor<WeaponType> craftPackageProcessor,
            EquipmentUpgradePackageProcessor equipmentUpgradePackageProcessor,
            SharpnessPackageProcessor sharpnessPackageProcessor,
            SwitchAxePhialPackageProcessor axePhials
        )
            : base(
                  WeaponType.SwitchAxe,
                  weaponsLanguages,
                  weaponsPackageProcessor,
                  craftPackageProcessor,
                  equipmentUpgradePackageProcessor,
                  sharpnessPackageProcessor
            )
        {
            this.axePhials = axePhials;
        }

        protected override void UpdateWeapon(MeleeWeaponPrimitiveBase weapon, SwitchAxe resultWeapon)
        {
            base.UpdateWeapon(weapon, resultWeapon);

            SwitchAxePhialPrimitive axePhial = axePhials.Table[weapon.Weapon1Id];

            int? damage = null;
            if (axePhial.Damage > 0)
                damage = axePhial.Damage * 10;

            resultWeapon.Phial = new SwitchAxePhial
            {
                Type = (SwitchAxePhialType)axePhial.PhialType,
                Damage = damage
            };
        }
    }
}
