using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MHWMasterDataUtils.Crafting;
using MHWMasterDataUtils.Equipments;
using MHWMasterDataUtils.Languages;
using MHWMasterDataUtils.Sharpness;
using MHWMasterDataUtils.Weapons;
using MHWMasterDataUtils.Weapons.HighLevel;
using MHWMasterDataUtils.Weapons.Primitives;

using core = MHWMasterDataUtils.Core;

namespace MHWMasterDataUtils.Builders
{
    public class SharpnessWeaponBuilder
    {
        private readonly LanguagePackageProcessor weaponsLanguages;
        private readonly SharpnessPackageProcessor sharpnessPackageProcessor;
        private readonly CraftPackageProcessor craftPackageProcessor;
        private readonly HuntingHornNotesPackageProcessor huntingHornNotes;
        private readonly HuntingHornSongsPackageProcessor huntingHornSongs;

        private readonly Dictionary<uint, WeaponPrimitiveBase> weapons;
        private readonly Dictionary<ushort, WeaponUpgradeEntryPrimitive> weaponUpgrades;

        private readonly Dictionary<(core.WeaponType, uint), uint> weaponIndices = new Dictionary<(core.WeaponType, uint), uint>();

        public core.WeaponType WeaponType { get; }

        public SharpnessWeaponBuilder(
            core.WeaponType weaponType,
            LanguagePackageProcessor weaponsLanguages,
            SharpnessPackageProcessor sharpnessPackageProcessor,
            WeaponsPackageProcessor weaponsPackageProcessor,
            CraftPackageProcessor craftPackageProcessor,
            WeaponUpgradePackageProcessor weaponUpgradePackageProcessor,
            HuntingHornNotesPackageProcessor huntingHornNotes,
            HuntingHornSongsPackageProcessor huntingHornSongs
        )
        {
            WeaponType = weaponType;

            this.weaponsLanguages = weaponsLanguages;
            this.sharpnessPackageProcessor = sharpnessPackageProcessor;
            this.craftPackageProcessor = craftPackageProcessor;
            this.huntingHornNotes = huntingHornNotes;
            this.huntingHornSongs = huntingHornSongs;

            weapons = weaponsPackageProcessor.Table[weaponType];
            weaponUpgrades = weaponUpgradePackageProcessor.Table[weaponType];

            CreateAllWeaponIndices(weaponsPackageProcessor.Table);
        }

        public enum WeaponOrder
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

        private static WeaponOrder ToWeaponOrder(core.WeaponType weaponType)
        {
            switch (weaponType)
            {
                case core.WeaponType.GreatSword: return WeaponOrder.GreatSword;
                case core.WeaponType.LongSword: return WeaponOrder.LongSword;
                case core.WeaponType.SwordAndShield: return WeaponOrder.SwordAndShield;
                case core.WeaponType.DualBlades: return WeaponOrder.DualBlades;
                case core.WeaponType.Hammer: return WeaponOrder.Hammer;
                case core.WeaponType.HuntingHorn: return WeaponOrder.HuntingHorn;
                case core.WeaponType.Lance: return WeaponOrder.Lance;
                case core.WeaponType.Gunlance: return WeaponOrder.Gunlance;
                case core.WeaponType.SwitchAxe: return WeaponOrder.SwitchAxe;
                case core.WeaponType.ChargeBlade: return WeaponOrder.ChargeBlade;
                case core.WeaponType.InsectGlaive: return WeaponOrder.InsectGlaive;
                case core.WeaponType.Bow: return WeaponOrder.Bow;
                case core.WeaponType.LightBowgun: return WeaponOrder.LightBowgun;
                case core.WeaponType.HeavyBowgun: return WeaponOrder.HeavyBowgun;
            }

            throw new ArgumentException($"Unknown weapon type '{weaponType}'.");
        }

        private void CreateAllWeaponIndices(Dictionary<core.WeaponType, Dictionary<uint, WeaponPrimitiveBase>> allWeapons)
        {
            uint index = 0;

            foreach (core.WeaponType weaponType in allWeapons.Keys.OrderBy(x => ToWeaponOrder(x)))
            {
                Dictionary<uint, WeaponPrimitiveBase> weapons = allWeapons[weaponType];

                foreach (WeaponPrimitiveBase weapon in weapons.Values.OrderBy(x => x.Id))
                {
                    if (weapon.TreeId == 0)
                        continue;

                    weaponIndices.Add((weapon.WeaponType, weapon.Id), ++index);
                }
            }
        }

        private bool IsSongNoteAvailable(HuntingHornNoteColor note, HuntingHornNotesPrimitive notes)
        {
            if (note == HuntingHornNoteColor.Disabled)
                return true;

            return note == notes.Note1 || note == notes.Note2 || note == notes.Note3;
        }

        private HuntingHornSongPrimitive[] FindSongs(HuntingHornNotesPrimitive notes)
        {
            var result = new List<HuntingHornSongPrimitive>();

            foreach (HuntingHornSongPrimitive song in huntingHornSongs.List)
            {
                if (IsSongNoteAvailable(song.Note1, notes) &&
                    IsSongNoteAvailable(song.Note2, notes) &&
                    IsSongNoteAvailable(song.Note3, notes) &&
                    IsSongNoteAvailable(song.Note4, notes))
                    result.Add(song);
            }

            return result.ToArray();
        }

        private bool IsValidWeapon(bool upgradable, MeleeWeaponPrimitiveBase weapon)
        {
            if (upgradable ^ weapon.TreeId > 0)
                return false;

            string englishWeaponName = weaponsLanguages.Table[LanguageIdPrimitive.English][weapon.GmdNameIndex].Value;
            if (LanguageUtils.IsValidText(englishWeaponName) == false)
                return false;

            string japaneseWeaponName = weaponsLanguages.Table[LanguageIdPrimitive.Japanese][weapon.GmdNameIndex].Value;
            if (LanguageUtils.IsValidText(japaneseWeaponName) == false)
                return false;

            if (sharpnessPackageProcessor.Table.ContainsKey(weapon.SharpnessId) == false)
                return false;

            return true;
        }

        private List<MeleeWeaponPrimitiveBase> CreateValidWeaponsList(bool upgradable)
        {
            var result = new List<MeleeWeaponPrimitiveBase>();

            foreach (MeleeWeaponPrimitiveBase weapon in weapons.Values)
            {
                if (IsValidWeapon(upgradable, weapon))
                    result.Add(weapon);
            }

            return result;
        }

        private int FindWeaponParentId(uint oneBasedWeaponIndex, List<MeleeWeaponPrimitiveBase> weapons)
        {
            foreach (MeleeWeaponPrimitiveBase weapon in weapons)
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

        public core.SharpnessWeapon[] Build()
        {
            var result = new List<core.SharpnessWeapon>();

            List<MeleeWeaponPrimitiveBase> nonUpgradableWeapons = CreateValidWeaponsList(false);
            List<MeleeWeaponPrimitiveBase> upgradableWeapons = CreateValidWeaponsList(true);

            foreach (MeleeWeaponPrimitiveBase weapon in upgradableWeapons)
            {
                uint oneBasedWeaponIndex = weaponIndices[(weapon.WeaponType, weapon.Id)];
                int parentId = FindWeaponParentId(oneBasedWeaponIndex, upgradableWeapons);

                core.SharpnessWeapon resultWeapon = CreateHighLevelWeapon(parentId, weapon);

                result.Add(resultWeapon);
            }

            foreach (MeleeWeaponPrimitiveBase weapon in nonUpgradableWeapons)
            {
                core.SharpnessWeapon resultWeapon = CreateHighLevelWeapon(-1, weapon);
                result.Add(resultWeapon);
            }

            return result.ToArray();
        }

        private core.SharpnessWeapon CreateHighLevelWeapon(int parentId, MeleeWeaponPrimitiveBase weapon)
        {
            Dictionary<string, string> weaponName = LanguageUtils.CreateLocalizations(weaponsLanguages.Table, weapon.GmdNameIndex);
            Dictionary<string, string> weaponDescription = LanguageUtils.CreateLocalizations(weaponsLanguages.Table, weapon.GmdDescriptionIndex);

            core.SharpnessInfo maxSharpness = sharpnessPackageProcessor.Table[weapon.SharpnessId];

            ushort sharpnessModifier = SharpnessUtils.ToSharpnessModifier(weapon.Handicraft);
            core.SharpnessInfo currentSharpness = SharpnessUtils.ApplySharpnessModifier(sharpnessModifier, maxSharpness);

            object weaponSpecific = null;

            if (WeaponType == core.WeaponType.HuntingHorn)
            {
                HuntingHornNotesPrimitive notes = huntingHornNotes.Table[weapon.Weapon1Id];
                weaponSpecific = FindSongs(notes);
            }

            var resultWeapon = new core.SharpnessWeapon(
                WeaponType,
                weapon.Id,
                parentId,
                weaponName,
                weaponDescription,
                WeaponsUtils.ComputeWeaponDamage(WeaponType, weapon.RawDamage),
                weapon.Rarity,
                weapon.TreeId,
                currentSharpness,
                maxSharpness,
                weapon.Affinity,
                weapon.CraftingCost,
                weapon.Defense,
                weapon.Elderseal,
                weapon.ElementId,
                (ushort)(weapon.ElementDamage * 10),
                weapon.HiddenElementId,
                (ushort)(weapon.HiddenElementDamage * 10),
                WeaponsUtils.CreateSlotsArray(weapon),
                weapon.IsFixedUpgrade == FixedUpgradePrimitive.CanDowngrade,
                weaponSpecific
            );

            return resultWeapon;
        }
    }
}
