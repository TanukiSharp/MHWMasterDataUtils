using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MHWMasterDataUtils.Weapons
{
    public class HuntingHornNotesPackageProcessor : MapPackageProcessorBase<uint, HuntingHornNotesPrimitive>
    {
        public HuntingHornNotesPackageProcessor()
            : base(new ushort[] { 0x01C1 }, HuntingHornNotesPrimitive.Read, x => x.SongId)
        {
        }

        public override bool IsChunkFileMatching(string chunkFullFilename)
        {
            return chunkFullFilename == "/common/equip/wep_whistle.wep_wsl";
        }
    }

    public class HuntingHornSongsPackageProcessor : ListPackageProcessorBase<HuntingHornSongPrimitive>
    {
        public override ICryptoInfo Crypto { get; } = new CryptoInfo("qm7psvaMXQoay7kARXpNPcLNWqsbqcOyI4lqHtxFh26HSuE6RHNq7J4e");

        public HuntingHornSongsPackageProcessor()
            : base(new ushort[] { 0x0190 }, HuntingHornSongPrimitive.Read)
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
