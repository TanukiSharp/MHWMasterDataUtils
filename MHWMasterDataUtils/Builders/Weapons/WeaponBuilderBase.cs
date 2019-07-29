using System;
using System.Collections.Generic;
using System.Linq;
using MHWMasterDataUtils.Crafting;
using MHWMasterDataUtils.Languages;
using MHWMasterDataUtils.Weapons;
using MHWMasterDataUtils.Weapons.Primitives;

using core = MHWMasterDataUtils.Core;

namespace MHWMasterDataUtils.Builders.Weapons
{
    public abstract class WeaponBuilderBase<TIntermediateWeapon, TResultWeapon>
        where TIntermediateWeapon: WeaponPrimitiveBase
        where TResultWeapon : core.WeaponBase, new()
    {
        public core.WeaponType WeaponType { get; }

        protected readonly LanguagePackageProcessor weaponsLanguages;
        protected readonly CraftPackageProcessor<core.WeaponType> craftPackageProcessor;

        protected readonly Dictionary<uint, WeaponPrimitiveBase> weapons;
        protected readonly Dictionary<ushort, WeaponUpgradeEntryPrimitive> weaponUpgrades;
        protected readonly Dictionary<(core.WeaponType, uint), uint> weaponIndices = new Dictionary<(core.WeaponType, uint), uint>();

        protected WeaponBuilderBase(
            core.WeaponType weaponType,
            LanguagePackageProcessor weaponsLanguages,
            WeaponsPackageProcessor weaponsPackageProcessor,
            CraftPackageProcessor<core.WeaponType> craftPackageProcessor,
            WeaponUpgradePackageProcessor weaponUpgradePackageProcessor
        )
        {
            WeaponType = weaponType;

            this.weaponsLanguages = weaponsLanguages;
            this.craftPackageProcessor = craftPackageProcessor;

            weapons = weaponsPackageProcessor.Table[weaponType];
            weaponUpgrades = weaponUpgradePackageProcessor.Table[weaponType];

            CreateAllWeaponIndices(weaponsPackageProcessor.Table);
        }

        public enum WeaponTypeOrder
        {
            GreatSword,
            LongSword,
            SwordAndShield,
            DualBlades,
            Hammer,
            HuntingHorn,
            Lance,
            Gunlance,
            SwitchAxe,
            ChargeBlade,
            InsectGlaive,
            Bow,
            HeavyBowgun,
            LightBowgun,
        }

        private static WeaponTypeOrder ToWeaponTypeOrder(core.WeaponType weaponType)
        {
            switch (weaponType)
            {
                case core.WeaponType.GreatSword: return WeaponTypeOrder.GreatSword;
                case core.WeaponType.LongSword: return WeaponTypeOrder.LongSword;
                case core.WeaponType.SwordAndShield: return WeaponTypeOrder.SwordAndShield;
                case core.WeaponType.DualBlades: return WeaponTypeOrder.DualBlades;
                case core.WeaponType.Hammer: return WeaponTypeOrder.Hammer;
                case core.WeaponType.HuntingHorn: return WeaponTypeOrder.HuntingHorn;
                case core.WeaponType.Lance: return WeaponTypeOrder.Lance;
                case core.WeaponType.Gunlance: return WeaponTypeOrder.Gunlance;
                case core.WeaponType.SwitchAxe: return WeaponTypeOrder.SwitchAxe;
                case core.WeaponType.ChargeBlade: return WeaponTypeOrder.ChargeBlade;
                case core.WeaponType.InsectGlaive: return WeaponTypeOrder.InsectGlaive;
                case core.WeaponType.Bow: return WeaponTypeOrder.Bow;
                case core.WeaponType.LightBowgun: return WeaponTypeOrder.LightBowgun;
                case core.WeaponType.HeavyBowgun: return WeaponTypeOrder.HeavyBowgun;
            }

            throw new ArgumentException($"Unknown weapon type '{weaponType}'.");
        }

        private void CreateAllWeaponIndices(Dictionary<core.WeaponType, Dictionary<uint, WeaponPrimitiveBase>> allWeapons)
        {
            uint index = 0;

            foreach (core.WeaponType weaponType in allWeapons.Keys.OrderBy(x => ToWeaponTypeOrder(x)))
            {
                Dictionary<uint, WeaponPrimitiveBase> weapons = allWeapons[weaponType];

                foreach (WeaponPrimitiveBase weapon in weapons.Values.OrderBy(x => x.TreeOrder))
                {
                    if (weapon.TreeId == 0)
                        continue;

                    weaponIndices.Add((weapon.WeaponType, weapon.Id), ++index);
                }
            }
        }

        private int FindWeaponParentId(uint oneBasedWeaponIndex, List<TIntermediateWeapon> weapons)
        {
            foreach (TIntermediateWeapon weapon in weapons)
            {
                if (weaponUpgrades.TryGetValue((ushort)weapon.Id, out WeaponUpgradeEntryPrimitive weaponUpgradeEntry) == false)
                    continue;

                if (weaponUpgradeEntry.Descendant1Id == oneBasedWeaponIndex ||
                    weaponUpgradeEntry.Descendant2Id == oneBasedWeaponIndex ||
                    weaponUpgradeEntry.Descendant3Id == oneBasedWeaponIndex ||
                    weaponUpgradeEntry.Descendant4Id == oneBasedWeaponIndex)
                    return (int)weapon.Id;
            }

            return -1;
        }

        protected virtual TResultWeapon CreateResultWeaponInstance()
        {
            return new TResultWeapon();
        }

        protected virtual bool IsValidWeapon(TIntermediateWeapon weapon)
        {
            return true;
        }

        protected abstract void UpdateWeapon(TIntermediateWeapon weapon, TResultWeapon resultWeapon);

        private bool IsValidWeaponInternal(bool upgradable, TIntermediateWeapon weapon)
        {
            if (upgradable ^ weapon.TreeId > 0)
                return false;

            string englishWeaponName = weaponsLanguages.Table[LanguageIdPrimitive.English][weapon.GmdNameIndex].Value;
            if (LanguageUtils.IsValidText(englishWeaponName) == false)
                return false;

            string japaneseWeaponName = weaponsLanguages.Table[LanguageIdPrimitive.Japanese][weapon.GmdNameIndex].Value;
            if (LanguageUtils.IsValidText(japaneseWeaponName) == false)
                return false;

            if (IsValidWeapon(weapon) == false)
                return false;

            return true;
        }

        private List<TIntermediateWeapon> CreateValidWeaponsList(bool upgradable)
        {
            var result = new List<TIntermediateWeapon>();

            foreach (TIntermediateWeapon weapon in weapons.Values)
            {
                if (IsValidWeaponInternal(upgradable, weapon))
                    result.Add(weapon);
            }

            return result;
        }

        private void CreateUpgradableWeapons(List<TResultWeapon> result)
        {
            List<TIntermediateWeapon> upgradableWeapons = CreateValidWeaponsList(true);

            foreach (TIntermediateWeapon weapon in upgradableWeapons)
            {
                uint oneBasedWeaponIndex = weaponIndices[(weapon.WeaponType, weapon.Id)];
                int parentId = FindWeaponParentId(oneBasedWeaponIndex, upgradableWeapons);

                TResultWeapon resultWeapon = CreateHighLevelWeapon(parentId, true, weapon);

                result.Add(resultWeapon);
            }
        }

        private void CreateNonUpgradableWeapons(List<TResultWeapon> result)
        {
            List<TIntermediateWeapon> nonUpgradableWeapons = CreateValidWeaponsList(false);

            foreach (TIntermediateWeapon weapon in nonUpgradableWeapons)
            {
                TResultWeapon resultWeapon = CreateHighLevelWeapon(-1, false, weapon);

                result.Add(resultWeapon);
            }
        }

        private static void TryAddCraft(List<core.CraftItem> crafts, ushort id, byte quantity)
        {
            if (quantity > 0)
                crafts.Add(new core.CraftItem { Id = id, Quantity = quantity });
        }

        private core.Craft CreateCraft(TIntermediateWeapon weapon)
        {
            bool isCraftable;
            var result = new List<core.CraftItem>();

            if (craftPackageProcessor.TryGetEntry(weapon.WeaponType, weapon.Id, out CraftEntryPrimitive craftEntry))
            {
                isCraftable = true;
                TryAddCraft(result, craftEntry.Item1Id, craftEntry.Item1Quantity);
                TryAddCraft(result, craftEntry.Item2Id, craftEntry.Item2Quantity);
                TryAddCraft(result, craftEntry.Item3Id, craftEntry.Item3Quantity);
                TryAddCraft(result, craftEntry.Item4Id, craftEntry.Item4Quantity);
            }
            else if (weaponUpgrades.TryGetValue((ushort)weapon.Id, out WeaponUpgradeEntryPrimitive upgradeEntry))
            {
                isCraftable = false;
                TryAddCraft(result, upgradeEntry.Item1Id, upgradeEntry.Item1Quantity);
                TryAddCraft(result, upgradeEntry.Item2Id, upgradeEntry.Item2Quantity);
                TryAddCraft(result, upgradeEntry.Item3Id, upgradeEntry.Item3Quantity);
                TryAddCraft(result, upgradeEntry.Item4Id, upgradeEntry.Item4Quantity);
            }
            else
                throw new FormatException($"Unknown weapon for craft of upgrade (type: {weapon.WeaponType}, id: {weapon.Id})");

            return new core.Craft
            {
                IsCraftable = isCraftable,
                Items = result.OrderBy(x => x.Id).ToArray()
            };
        }

        private TResultWeapon CreateHighLevelWeapon(int parentId, bool isUpgradable, TIntermediateWeapon weapon)
        {
            TResultWeapon resultWeapon = CreateResultWeaponInstance();

            Dictionary<string, string> weaponName = LanguageUtils.CreateLocalizations(weaponsLanguages.Table, weapon.GmdNameIndex);
            Dictionary<string, string> weaponDescription = LanguageUtils.CreateLocalizations(weaponsLanguages.Table, weapon.GmdDescriptionIndex, true);

            core.Craft craft = null;

            if (isUpgradable)
                craft = CreateCraft(weapon);

            bool canDowngrade = false;
            if (parentId > -1)
                canDowngrade = weapon.IsFixedUpgrade == FixedUpgradePrimitive.CanDowngrade;

            resultWeapon.Id = weapon.Id;
            resultWeapon.TreeOrder = weapon.TreeOrder;
            resultWeapon.ParentId = parentId;
            resultWeapon.Name = weaponName;
            resultWeapon.Description = weaponDescription;
            resultWeapon.Damage = WeaponsUtils.ComputeWeaponDamage(WeaponType, weapon.RawDamage);
            resultWeapon.Rarity = weapon.Rarity;
            resultWeapon.TreeId = weapon.TreeId;
            resultWeapon.Affinity = weapon.Affinity;
            resultWeapon.CraftingCost = weapon.CraftingCost;
            resultWeapon.Defense = weapon.Defense;
            resultWeapon.Elderseal = weapon.Elderseal;
            resultWeapon.ElementStatus = weapon.ElementId;
            resultWeapon.ElementStatusDamage = (ushort)(weapon.ElementDamage * 10);
            resultWeapon.HiddenElementStatus = weapon.HiddenElementId;
            resultWeapon.HiddenElementStatusDamage = (ushort)(weapon.HiddenElementDamage * 10);
            resultWeapon.SkillId = weapon.SkillId;
            resultWeapon.Slots = WeaponsUtils.CreateSlotsArray(weapon);
            resultWeapon.CanDowngrade = canDowngrade;
            resultWeapon.Craft = craft;

            UpdateWeapon(weapon, resultWeapon);

            return resultWeapon;
        }

        public TResultWeapon[] Build()
        {
            var result = new List<TResultWeapon>();

            CreateUpgradableWeapons(result);
            CreateNonUpgradableWeapons(result);

            result.Sort((a, b) => a.TreeOrder.CompareTo(b.TreeOrder));

            return result.ToArray();
        }
    }
}
