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
    public abstract class MeleeWeaponBuilderBase : WeaponBuilderBase
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

        protected override bool IsValidSpecificWeapon(WeaponPrimitiveBase weapon)
        {
            var typedWeapon = weapon as MeleeWeaponPrimitiveBase;

            if (typedWeapon == null)
                return false;

            if (sharpnessPackageProcessor.Table.ContainsKey(typedWeapon.SharpnessId) == false)
                return false;

            return true;
        }

        protected virtual object CreateWeaponSpecificValue(MeleeWeaponPrimitiveBase weapon)
        {
            return null;
        }

        protected override core.WeaponBase BuildSpecificWeapon(ref WeaponComputedArguments computedArguments, WeaponPrimitiveBase weapon)
        {
            var typedWeapon = (MeleeWeaponPrimitiveBase)weapon;

            core.SharpnessInfo maxSharpness = sharpnessPackageProcessor.Table[typedWeapon.SharpnessId];

            ushort sharpnessModifier = SharpnessUtils.ToSharpnessModifier(typedWeapon.Handicraft);
            core.SharpnessInfo sharpness = SharpnessUtils.ApplySharpnessModifier(sharpnessModifier, maxSharpness);

            object weaponSpecific = CreateWeaponSpecificValue(typedWeapon);

            var resultWeapon = new core.MeleeWeapon(
                WeaponType,
                weapon.Id,
                weapon.TreeOrder,
                computedArguments.ParentId,
                computedArguments.Name,
                computedArguments.Description,
                WeaponsUtils.ComputeWeaponDamage(WeaponType, weapon.RawDamage),
                weapon.Rarity,
                weapon.TreeId,
                sharpness,
                maxSharpness,
                weapon.Affinity,
                weapon.CraftingCost,
                weapon.Defense,
                weapon.Elderseal,
                weapon.ElementId,
                (ushort)(weapon.ElementDamage * 10),
                weapon.HiddenElementId,
                (ushort)(weapon.HiddenElementDamage * 10),
                weapon.SkillId,
                WeaponsUtils.CreateSlotsArray(weapon),
                computedArguments.CanDowngrade,
                weaponSpecific,
                computedArguments.Craft
            );

            return resultWeapon;
        }
    }
}
