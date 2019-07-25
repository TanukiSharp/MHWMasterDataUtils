using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MHWMasterDataUtils.Crafting;
using MHWMasterDataUtils.Equipments;
using MHWMasterDataUtils.Languages;
using MHWMasterDataUtils.Sharpness;
using MHWMasterDataUtils.Weapons;
using MHWMasterDataUtils.Weapons.Primitives;

using core = MHWMasterDataUtils.Core;

namespace MHWMasterDataUtils.Builders
{
    public class SharpnessWeaponBuilder
    {
        private readonly LanguagePackageProcessor weaponsLanguages;
        private readonly SharpnessPackageProcessor sharpnessPackageProcessor;
        private readonly CraftPackageProcessor<core.WeaponType> craftPackageProcessor;
        private readonly HuntingHornNotesPackageProcessor huntingHornNotes;
        private readonly HuntingHornSongsPackageProcessor huntingHornSongs;
        private readonly DualBladesSpecialPackageProcessor dualBladesSpecial;
        private readonly AxePhialPackageProcessor axePhials;
        private readonly GunlanceShellPackageProcessor gunlanceShells;

        private readonly Dictionary<uint, WeaponPrimitiveBase> weapons;
        private readonly Dictionary<ushort, WeaponUpgradeEntryPrimitive> weaponUpgrades;

        private readonly Dictionary<(core.WeaponType, uint), uint> weaponIndices = new Dictionary<(core.WeaponType, uint), uint>();

        public core.WeaponType WeaponType { get; }

        public SharpnessWeaponBuilder(
            core.WeaponType weaponType,
            LanguagePackageProcessor weaponsLanguages,
            SharpnessPackageProcessor sharpnessPackageProcessor,
            WeaponsPackageProcessor weaponsPackageProcessor,
            CraftPackageProcessor<core.WeaponType> craftPackageProcessor,
            WeaponUpgradePackageProcessor weaponUpgradePackageProcessor,
            HuntingHornNotesPackageProcessor huntingHornNotes,
            HuntingHornSongsPackageProcessor huntingHornSongs,
            DualBladesSpecialPackageProcessor dualBladesSpecial,
            AxePhialPackageProcessor axePhials,
            GunlanceShellPackageProcessor gunlanceShells
        )
        {
            WeaponType = weaponType;

            this.weaponsLanguages = weaponsLanguages;
            this.sharpnessPackageProcessor = sharpnessPackageProcessor;
            this.craftPackageProcessor = craftPackageProcessor;
            this.huntingHornNotes = huntingHornNotes;
            this.huntingHornSongs = huntingHornSongs;
            this.dualBladesSpecial = dualBladesSpecial;
            this.axePhials = axePhials;
            this.gunlanceShells = gunlanceShells;
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

        private bool IsSongNoteAvailable(core.HuntingHornNoteColor note, HuntingHornNotesPrimitive notes)
        {
            if (note == core.HuntingHornNoteColor.Disabled)
                return true;

            return note == notes.Note1 || note == notes.Note2 || note == notes.Note3;
        }

        private static string ConvertHuntingHornNote(core.HuntingHornNoteColor note)
        {
            if (note == core.HuntingHornNoteColor.Disabled)
                return null;

            return note.ToString();
        }

        private static core.HuntingHornSong ConvertHuntingHornSong(HuntingHornSongPrimitive song)
        {
            return new core.HuntingHornSong
            {
                Effect = song.Effect.ToString(),
                Note1 = ConvertHuntingHornNote(song.Note1),
                Note2 = ConvertHuntingHornNote(song.Note2),
                Note3 = ConvertHuntingHornNote(song.Note3),
                Note4 = ConvertHuntingHornNote(song.Note4),
            };
        }

        private core.HuntingHornSong[] FindSongs(HuntingHornNotesPrimitive notes)
        {
            var result = new List<core.HuntingHornSong>();

            foreach (HuntingHornSongPrimitive song in huntingHornSongs.List)
            {
                if (IsSongNoteAvailable(song.Note1, notes) &&
                    IsSongNoteAvailable(song.Note2, notes) &&
                    IsSongNoteAvailable(song.Note3, notes) &&
                    IsSongNoteAvailable(song.Note4, notes))
                    result.Add(ConvertHuntingHornSong(song));
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

            CreateUpgradableWeapons(result);
            CreateNonUpgradableWeapons(result);

            result.Sort((a, b) => a.TreeOrder.CompareTo(b.TreeOrder));

            return result.ToArray();
        }

        private void CreateUpgradableWeapons(List<core.SharpnessWeapon> result)
        {
            List<MeleeWeaponPrimitiveBase> upgradableWeapons = CreateValidWeaponsList(true);

            foreach (MeleeWeaponPrimitiveBase weapon in upgradableWeapons)
            {
                uint oneBasedWeaponIndex = weaponIndices[(weapon.WeaponType, weapon.Id)];
                int parentId = FindWeaponParentId(oneBasedWeaponIndex, upgradableWeapons);

                core.SharpnessWeapon resultWeapon = CreateHighLevelWeapon(parentId, CreateCraft(weapon), weapon);

                result.Add(resultWeapon);
            }
        }

        private void CreateNonUpgradableWeapons(List<core.SharpnessWeapon> result)
        {
            List<MeleeWeaponPrimitiveBase> nonUpgradableWeapons = CreateValidWeaponsList(false);

            foreach (MeleeWeaponPrimitiveBase weapon in nonUpgradableWeapons)
            {
                core.SharpnessWeapon resultWeapon = CreateHighLevelWeapon(-1, null, weapon);
                result.Add(resultWeapon);
            }
        }

        private static void TryAddCraft(List<core.CraftItem> crafts, ushort id, byte quantity)
        {
            if (quantity > 0)
                crafts.Add(new core.CraftItem { Id = id, Quantity = quantity });
        }

        private core.Craft CreateCraft(MeleeWeaponPrimitiveBase weapon)
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

        private core.SharpnessWeapon CreateHighLevelWeapon(int parentId, core.Craft craft, MeleeWeaponPrimitiveBase weapon)
        {
            Dictionary<string, string> weaponName = LanguageUtils.CreateLocalizations(weaponsLanguages.Table, weapon.GmdNameIndex);
            Dictionary<string, string> weaponDescription = LanguageUtils.CreateLocalizations(weaponsLanguages.Table, weapon.GmdDescriptionIndex);

            core.SharpnessInfo maxSharpness = sharpnessPackageProcessor.Table[weapon.SharpnessId];

            ushort sharpnessModifier = SharpnessUtils.ToSharpnessModifier(weapon.Handicraft);
            core.SharpnessInfo sharpness = SharpnessUtils.ApplySharpnessModifier(sharpnessModifier, maxSharpness);

            object weaponSpecific = null;

            if (WeaponType == core.WeaponType.HuntingHorn)
            {
                HuntingHornNotesPrimitive notes = huntingHornNotes.Table[weapon.Weapon1Id];
                weaponSpecific = FindSongs(notes);
            }
            else if (WeaponType == core.WeaponType.DualBlades)
            {
                if (weapon.Weapon1Id > 0)
                {
                    DualBladesSpecialPrimitive dualBladesElementInfo = dualBladesSpecial.Table[weapon.Weapon1Id];

                    weaponSpecific = new
                    {
                        elementStatus1 = (int)dualBladesElementInfo.Element1, // Matches type core.ElementStatus.
                        elementStatus1Damage = dualBladesElementInfo.Element1Damage * 10,
                        elementStatus2 = (int)dualBladesElementInfo.Element2, // Matches type core.ElementStatus.
                        elementStatus2Damage = dualBladesElementInfo.Element2Damage * 10,
                    };
                }
            }
            else if (WeaponType == core.WeaponType.Gunlance)
            {
                GunlanceShellPrimitive gunlanceShell = gunlanceShells.Table[weapon.Weapon1Id];

                weaponSpecific = new core.GunlanceShell
                {
                    ShellType = (int)gunlanceShell.ShellType, // Matches type core.GunlanceShellType.
                    ShellLevel = gunlanceShell.ShellLevel + 1
                };
            }
            else if (WeaponType == core.WeaponType.SwitchAxe || WeaponType == core.WeaponType.ChargeBlade)
            {
                AxePhialPrimitive axePhial = axePhials.Table[weapon.Weapon1Id];
                weaponSpecific = new core.AxePhial
                {
                    ElementStatus = (int)axePhial.ElementStatus,
                    Damage = axePhial.Damage * 10
                };
            }
            else if (WeaponType == core.WeaponType.InsectGlaive)
            {
                weaponSpecific = (int)weapon.Weapon1Id; // Matches type core.KinsectBonus.
            }

            bool canDowngrade = false;
            if (parentId > -1)
                canDowngrade = weapon.IsFixedUpgrade == FixedUpgradePrimitive.CanDowngrade;

            var resultWeapon = new core.SharpnessWeapon(
                WeaponType,
                weapon.Id,
                weapon.TreeOrder,
                parentId,
                weaponName,
                weaponDescription,
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
                canDowngrade,
                weaponSpecific,
                craft
            );

            return resultWeapon;
        }
    }
}
