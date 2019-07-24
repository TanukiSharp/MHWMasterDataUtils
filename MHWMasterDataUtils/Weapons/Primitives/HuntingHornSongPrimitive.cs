using System;
using System.Collections.Generic;
using System.Text;
using MHWMasterDataUtils.Core;
using Newtonsoft.Json;

namespace MHWMasterDataUtils.Weapons.Primitives
{
    public class HuntingHornSongPrimitive
    {
        public readonly HuntingHornSongEffect Effect;
        public readonly HuntingHornNoteColor Note1;
        public readonly HuntingHornNoteColor Note2;
        public readonly HuntingHornNoteColor Note3;
        public readonly HuntingHornNoteColor Note4;

        private HuntingHornSongPrimitive(
            HuntingHornSongEffect effect,
            HuntingHornNoteColor note1,
            HuntingHornNoteColor note2,
            HuntingHornNoteColor note3,
            HuntingHornNoteColor note4
        )
        {
            Effect = effect;
            Note1 = note1;
            Note2 = note2;
            Note3 = note3;
            Note4 = note4;
        }

        public static HuntingHornSongPrimitive Read(Reader reader)
        {
            var effect = (HuntingHornSongEffect)reader.ReadUInt32();
            var note1 = (HuntingHornNoteColor)reader.ReadInt32();
            var note2 = (HuntingHornNoteColor)reader.ReadInt32();
            var note3 = (HuntingHornNoteColor)reader.ReadInt32();
            var note4 = (HuntingHornNoteColor)reader.ReadInt32();

            return new HuntingHornSongPrimitive(effect, note1, note2, note3, note4);
        }

        public override string ToString()
        {
            var result = new List<string>();

            if (Note1 != HuntingHornNoteColor.Disabled)
                result.Add(Note1.ToString());
            if (Note2 != HuntingHornNoteColor.Disabled)
                result.Add(Note2.ToString());
            if (Note3 != HuntingHornNoteColor.Disabled)
                result.Add(Note3.ToString());
            if (Note4 != HuntingHornNoteColor.Disabled)
                result.Add(Note4.ToString());

            return $"[{Effect}] {string.Join(", ", result)}";
        }
    }
}
