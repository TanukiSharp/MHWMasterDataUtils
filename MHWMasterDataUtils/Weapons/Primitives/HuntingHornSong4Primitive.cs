using System;
using System.Collections.Generic;
using System.Text;

namespace MHWMasterDataUtils.Weapons.Primitives
{
    public class HuntingHornSong4Primitive
    {
        public readonly uint SongId;
        public readonly HuntingHornNoteColorPrimitive Note1;
        public readonly HuntingHornNoteColorPrimitive Note2;
        public readonly HuntingHornNoteColorPrimitive Note3;
        public readonly HuntingHornNoteColorPrimitive Note4;

        private HuntingHornSong4Primitive(
            uint songId,
            HuntingHornNoteColorPrimitive note1,
            HuntingHornNoteColorPrimitive note2,
            HuntingHornNoteColorPrimitive note3,
            HuntingHornNoteColorPrimitive note4
        )
        {
            SongId = songId;
            Note1 = note1;
            Note2 = note2;
            Note3 = note3;
            Note4 = note4;
        }

        public static HuntingHornSong4Primitive Read(Reader reader)
        {
            uint songId = reader.ReadUInt32();
            var note1 = (HuntingHornNoteColorPrimitive)reader.ReadInt32();
            var note2 = (HuntingHornNoteColorPrimitive)reader.ReadInt32();
            var note3 = (HuntingHornNoteColorPrimitive)reader.ReadInt32();
            var note4 = (HuntingHornNoteColorPrimitive)reader.ReadInt32();

            return new HuntingHornSong4Primitive(songId, note1, note2, note3, note4);
        }
    }
}
