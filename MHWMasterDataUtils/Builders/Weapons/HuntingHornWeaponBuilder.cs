using System;
using System.Collections.Generic;
using MHWMasterDataUtils.Core;
using MHWMasterDataUtils.Equipment;
using MHWMasterDataUtils.Languages;
using MHWMasterDataUtils.Sharpness;
using MHWMasterDataUtils.Weapons;

namespace MHWMasterDataUtils.Builders.Weapons
{
    public class HuntingHornWeaponBuilder : MeleeWeaponBuilderBase<HuntingHorn>
    {
        private readonly HuntingHornNotesPackageProcessor huntingHornNotes;
        private readonly HuntingHornSongsPackageProcessor huntingHornSongs;

        public HuntingHornWeaponBuilder(
            LanguagePackageProcessor weaponsLanguages,
            WeaponsPackageProcessor weaponsPackageProcessor,
            EquipmentCraftPackageProcessor<WeaponType> craftPackageProcessor,
            EquipmentUpgradePackageProcessor equipmentUpgradePackageProcessor,
            SharpnessPackageProcessor sharpnessPackageProcessor,
            HuntingHornNotesPackageProcessor huntingHornNotes,
            HuntingHornSongsPackageProcessor huntingHornSongs
        )
            : base(
                  WeaponType.HuntingHorn,
                  weaponsLanguages,
                  weaponsPackageProcessor,
                  craftPackageProcessor,
                  equipmentUpgradePackageProcessor,
                  sharpnessPackageProcessor
            )
        {
            this.huntingHornNotes = huntingHornNotes;
            this.huntingHornSongs = huntingHornSongs;
        }

        private bool IsSongNoteAvailable(HuntingHornNoteColor note, HuntingHornNotesPrimitive notes)
        {
            if (note == HuntingHornNoteColor.Disabled)
                return true;

            return note == notes.Note1 || note == notes.Note2 || note == notes.Note3;
        }

        private static int? ConvertHuntingHornNote(HuntingHornNoteColor note)
        {
            if (note == HuntingHornNoteColor.Disabled)
                return null;

            return (int)note;
        }

        private static HuntingHornSong ConvertHuntingHornSong(HuntingHornSongPrimitive song)
        {
            return new HuntingHornSong
            {
                Effect = (int)song.Effect,
                Note1 = ConvertHuntingHornNote(song.Note1),
                Note2 = ConvertHuntingHornNote(song.Note2),
                Note3 = ConvertHuntingHornNote(song.Note3),
                Note4 = ConvertHuntingHornNote(song.Note4),
            };
        }

        private HuntingHornSong[] FindSongs(HuntingHornNotesPrimitive notes)
        {
            var result = new List<HuntingHornSong>();

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

        protected override void UpdateWeapon(MeleeWeaponPrimitiveBase weapon, HuntingHorn resultWeapon)
        {
            base.UpdateWeapon(weapon, resultWeapon);

            HuntingHornNotesPrimitive notes = huntingHornNotes.Table[weapon.Weapon1Id];
            resultWeapon.Songs = FindSongs(notes);
        }
    }
}
