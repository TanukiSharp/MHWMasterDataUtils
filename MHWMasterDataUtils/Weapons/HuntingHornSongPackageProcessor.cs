using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MHWMasterDataUtils.Weapons.Primitives;

namespace MHWMasterDataUtils.Weapons
{
    public class HuntingHornNotesPackageProcessor : SimpleMapPackageProcessorBase<uint, HuntingHornNotesPrimitive>
    {
        public HuntingHornNotesPackageProcessor()
            : base(0x0177, HuntingHornNotesPrimitive.Read, x => x.SongId)
        {
        }

        public override bool IsChunkFileMatching(string chunkFullFilename)
        {
            return chunkFullFilename == "/common/equip/wep_whistle.wep_wsl";
        }
    }

    public class HuntingHornSongsPackageProcessor : SimpleListPackageProcessorBase<HuntingHornSongPrimitive>
    {
        public HuntingHornSongsPackageProcessor()
            : base(0x0146, HuntingHornSongPrimitive.Read)
        {
        }

        public override bool IsChunkFileMatching(string chunkFullFilename)
        {
            return chunkFullFilename == "/hm/wp/wp05/music_skill.msk";
        }

        public override void PostProcess()
        {
            List.Sort((x, y) => x.Effect.CompareTo(y.Effect));
            base.PostProcess();
        }
    }
}
