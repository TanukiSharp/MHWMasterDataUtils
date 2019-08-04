using System;
using System.Collections.Generic;
using System.Text;

namespace MHWMasterDataUtils.Weapons
{
    public class GunlanceShellPackageProcessor : MapPackageProcessorBase<ushort, GunlanceShellPrimitive>
    {
        public GunlanceShellPackageProcessor()
            : base(0x0177, GunlanceShellPrimitive.Read, x => x.Id)
        {
        }

        public override bool IsChunkFileMatching(string chunkFullFilename)
        {
            return chunkFullFilename == "/common/equip/wep_glan.wep_glan";
        }
    }
}
