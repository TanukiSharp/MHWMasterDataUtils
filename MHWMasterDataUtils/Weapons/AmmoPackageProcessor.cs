using System;
using System.Collections.Generic;
using System.Text;
using MHWMasterDataUtils.Weapons.Primitives;

namespace MHWMasterDataUtils.Weapons
{
    public class AmmoPackageProcessor : SimpleListPackageProcessorBase<AmmoTableEntryPrimitive>
    {
        public AmmoPackageProcessor()
            : base(0x01A6, AmmoTableEntryPrimitive.Read)
        {
        }

        public override bool IsChunkFileMatching(string chunkFullFilename)
        {
            return chunkFullFilename == "/common/equip/shell_table.shl_tbl";
        }
    }
}
