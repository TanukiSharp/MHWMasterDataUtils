using System;
using System.Collections.Generic;
using System.Text;
using MHWMasterDataUtils.Weapons.Primitives;

namespace MHWMasterDataUtils.Weapons
{
    public class SwitchAxePhialPackageProcessor : SimpleMapPackageProcessorBase<ushort, SwitchAxePhialPrimitive>
    {
        public SwitchAxePhialPackageProcessor()
            : base(0x0177, SwitchAxePhialPrimitive.Read, x => x.Id)
        {
        }

        public override bool IsChunkFileMatching(string chunkFullFilename)
        {
            return chunkFullFilename == "/common/equip/wep_saxe.wep_saxe";
        }
    }
}
