using System;
using System.Collections.Generic;
using System.Text;

namespace MHWMasterDataUtils.Weapons
{
    public class DualBladesSpecialPackageProcessor : SimpleMapPackageProcessorBase<ushort, DualBladesSpecialPrimitive>
    {
        public DualBladesSpecialPackageProcessor()
            : base(0x0177, DualBladesSpecialPrimitive.Read, x => x.Id)
        {
        }

        public override bool IsChunkFileMatching(string chunkFullFilename)
        {
            return chunkFullFilename == "/common/equip/wep_wsword.wep_wsd";
        }
    }
}
