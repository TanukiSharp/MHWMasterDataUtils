using System;
using System.Collections.Generic;
using System.Text;
using MHWMasterDataUtils.Weapons.Primitives;

namespace MHWMasterDataUtils.Weapons
{
    public class AxePhialPackageProcessor : SimpleMapPackageProcessorBase<ushort, AxePhialPrimitive>
    {
        public AxePhialPackageProcessor()
            : base(0x0177, AxePhialPrimitive.Read, x => x.Id)
        {
        }

        public override bool IsChunkFileMatching(string chunkFullFilename)
        {
            return chunkFullFilename == "/common/equip/wep_saxe.wep_saxe";
        }
    }
}
