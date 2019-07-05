using System;
using System.Collections.Generic;
using System.Text;

namespace MHWMasterDataUtils.Weapons.Primitives
{
    public class HuntingHornSong3Primitive
    {
        public readonly uint SongId;
        public readonly HuntingHornNoteColorPrimitive Note1;
        public readonly HuntingHornNoteColorPrimitive Note2;
        public readonly HuntingHornNoteColorPrimitive Note3;

        private HuntingHornSong3Primitive(
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
        }

        public static HuntingHornSong3Primitive Read(Reader reader)
        {
            uint songId = reader.ReadUInt32();
            var note1 = (HuntingHornNoteColorPrimitive)reader.ReadByte();
            var note2 = (HuntingHornNoteColorPrimitive)reader.ReadByte();
            var note3 = (HuntingHornNoteColorPrimitive)reader.ReadByte();
            var note4 = (HuntingHornNoteColorPrimitive)reader.ReadByte();

            return new HuntingHornSong3Primitive(songId, note1, note2, note3, note4);
        }
    }
}
