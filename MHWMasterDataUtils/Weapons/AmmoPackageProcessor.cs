using System;
using System.Collections.Generic;
using System.Text;

namespace MHWMasterDataUtils.Weapons
{
    public class AmmoPackageProcessor : ListPackageProcessorBase<AmmoTableEntryPrimitive>
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
