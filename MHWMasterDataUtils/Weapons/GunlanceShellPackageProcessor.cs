using System;
using System.Collections.Generic;
using System.Text;

namespace MHWMasterDataUtils.Weapons
{
    public class GunlanceShellPackageProcessor : MapPackageProcessorBase<ushort, GunlanceShellPrimitive>
    {
        public GunlanceShellPackageProcessor()
            : base(new ushort[] { 0x01C2 }, GunlanceShellPrimitive.Read, x => x.Id)
        {
        }

        public override bool IsChunkFileMatching(string chunkFullFilename)
        {
            return chunkFullFilename == "/common/equip/wep_glan.wep_glan";
        }
    }
}
