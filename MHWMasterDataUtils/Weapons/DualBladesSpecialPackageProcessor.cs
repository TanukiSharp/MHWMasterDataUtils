using System;
using System.Collections.Generic;
using System.Text;

namespace MHWMasterDataUtils.Weapons
{
    public class DualBladesSpecialPackageProcessor : MapPackageProcessorBase<ushort, DualBladesSpecialPrimitive>
    {
        public DualBladesSpecialPackageProcessor()
            : base(new ushort[] { 0x01C1 }, DualBladesSpecialPrimitive.Read, x => x.Id)
        {
        }

        public override bool IsChunkFileMatching(string chunkFullFilename)
        {
            return chunkFullFilename == "/common/equip/wep_wsword.wep_wsd";
        }
    }
}
