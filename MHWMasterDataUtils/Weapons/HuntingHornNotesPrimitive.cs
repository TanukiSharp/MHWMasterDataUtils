using System;
using System.Collections.Generic;
using System.Text;
using MHWMasterDataUtils.Core;

namespace MHWMasterDataUtils.Weapons
{
    public class HuntingHornNotesPrimitive
    {
        public readonly uint SongId;
        public readonly HuntingHornNoteColor Note1;
        public readonly HuntingHornNoteColor Note2;
        public readonly HuntingHornNoteColor Note3;

        private HuntingHornNotesPrimitive(
            uint songId,
            HuntingHornNoteColor note1,
            HuntingHornNoteColor note2,
            HuntingHornNoteColor note3
        )
        {
            SongId = songId;
            Note1 = note1;
            Note2 = note2;
            Note3 = note3;
        }

        public static HuntingHornNotesPrimitive Read(Reader reader)
        {
            uint songId = reader.ReadUInt32();
            var note1 = (HuntingHornNoteColor)reader.ReadByte();
            var note2 = (HuntingHornNoteColor)reader.ReadByte();
            var note3 = (HuntingHornNoteColor)reader.ReadByte();

            return new HuntingHornNotesPrimitive(songId, note1, note2, note3);
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

            return string.Join(", ", result);
        }
    }
}
