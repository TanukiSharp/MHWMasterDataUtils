using System;
using System.Collections.Generic;
using System.Text;

namespace MHWMasterDataUtils.Weapons.HighLevel
{
    public class HuntingHornSong
    {
        public HuntingHornSongEffect Effet { get; }
        public HuntingHornNoteColor Note1 { get; }
        public HuntingHornNoteColor Note2 { get; }
        public HuntingHornNoteColor Note3 { get; }

        public HuntingHornSong(
            HuntingHornSongEffect effect,
            HuntingHornNoteColor note1,
            HuntingHornNoteColor note2,
            HuntingHornNoteColor note3
        )
        {
            Effet = effect;
            Note1 = note1;
            Note2 = note2;
            Note3 = note3;
        }
    }
}
