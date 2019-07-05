using System;
using System.Collections.Generic;
using System.Text;
using MHWMasterDataUtils.Weapons.Primitives;

namespace MHWMasterDataUtils.Weapons
{
    public class HuntingHornSong3PackageReader : SimpleMapPackageProcessorBase<uint, HuntingHornSong3Primitive>
    {
        public HuntingHornSong3PackageReader()
            : base(0x0177, HuntingHornSong3Primitive.Read, x => x.SongId)
        {
        }

        public override bool IsChunkFileMatching(string chunkFullFilename)
        {
            return chunkFullFilename == "/common/equip/wep_whistle.wep_wsl";
        }
    }

    public class HuntingHornSong4PackageReader : SimpleMapPackageProcessorBase<uint, HuntingHornSong4Primitive>
    {
        public HuntingHornSong4PackageReader()
            : base(0x0146, HuntingHornSong4Primitive.Read, x => x.SongId)
        {
        }

        public override bool IsChunkFileMatching(string chunkFullFilename)
        {
            return chunkFullFilename == "/hm/wp/wp05/music_skill.msk";
        }
    }
}
