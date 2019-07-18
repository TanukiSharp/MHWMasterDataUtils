using System;
using System.Collections.Generic;
using System.Text;
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
        private readonly WeaponsPackageProcessor weaponsPackageProcessor;
        private readonly HuntingHornNotesPackageProcessor huntingHornNotes;
        private readonly HuntingHornSongsPackageProcessor huntingHornSongs;

        public WeaponClass WeaponClass { get; }

        public SharpnessWeaponBuilder(
            WeaponClass weaponClass,
            LanguagePackageProcessor weaponsLanguages,
            SharpnessPackageProcessor sharpnessPackageProcessor,
            WeaponsPackageProcessor weaponsPackageProcessor,
            HuntingHornNotesPackageProcessor huntingHornNotes,
            HuntingHornSongsPackageProcessor huntingHornSongs
        )
        {
            WeaponClass = weaponClass;

            this.weaponsLanguages = weaponsLanguages;
            this.sharpnessPackageProcessor = sharpnessPackageProcessor;
            this.weaponsPackageProcessor = weaponsPackageProcessor;
            this.huntingHornNotes = huntingHornNotes;
            this.huntingHornSongs = huntingHornSongs;
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

        public core.SharpnessWeapon[] Build()
        {
            var result = new List<core.SharpnessWeapon>();

            foreach (KeyValuePair<uint, WeaponPrimitiveBase> keyValue in weaponsPackageProcessor.Table[WeaponClass])
            {
                uint weaponId = keyValue.Key;
                var weapon = (MeleeWeaponPrimitiveBase)keyValue.Value;

                Dictionary<string, string> weaponName = LanguageUtils.CreateLocalizations(weaponsLanguages.Table, weapon.GmdNameIndex);
                Dictionary<string, string> weaponDescription = LanguageUtils.CreateLocalizations(weaponsLanguages.Table, weapon.GmdDescriptionIndex);

                if (weaponName == null || weaponDescription == null || LanguageUtils.IsValidText(weaponName) == false)
                    continue;

                if (sharpnessPackageProcessor.Table.TryGetValue(weapon.SharpnessId, out core.SharpnessInfo sharpnessInfo) == false)
                    continue;

                core.SharpnessInfo maxSharpness = sharpnessPackageProcessor.Table[weapon.SharpnessId];

                ushort sharpnessModifier = SharpnessUtils.ToSharpnessModifier(weapon.Handicraft);
                core.SharpnessInfo currentSharpness = SharpnessUtils.ApplySharpnessModifier(sharpnessModifier, maxSharpness);

                object weaponSpecific = null;

                if (WeaponClass == WeaponClass.HuntingHorn)
                {
                    if (weapon.Weapon1Id > 0)
                    {
                        HuntingHornNotesPrimitive notes = huntingHornNotes.Table[weapon.Weapon1Id];
                        weaponSpecific = FindSongs(notes);
                    }
                    else if (weapon.Weapon2Id != 0)
                    {
                    }
                }

                var resultWeapon = new core.SharpnessWeapon(
                    WeaponsUtils.FromWeaponClass(WeaponClass),
                    weaponId,
                    weaponName,
                    weaponDescription,
                    WeaponsUtils.ComputeWeaponDamage(WeaponClass, weapon.RawDamage),
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

                result.Add(resultWeapon);
            }

            return result.ToArray();
        }
    }
}
