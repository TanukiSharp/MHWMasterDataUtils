using System;
using System.Collections.Generic;
using System.Text;

namespace MHWMasterDataUtils.Weapons
{
    public class SwitchAxePhialPackageProcessor : MapPackageProcessorBase<ushort, SwitchAxePhialPrimitive>
    {
        public SwitchAxePhialPackageProcessor()
            : base(new ushort[] { 0x01C1 }, SwitchAxePhialPrimitive.Read, x => x.Id)
        {
        }

        public override bool IsChunkFileMatching(string chunkFullFilename)
        {
            return chunkFullFilename == "/common/equip/wep_saxe.wep_saxe";
        }
    }
}
