using System;
using System.Collections.Generic;
using System.Text;

namespace MHWMasterDataUtils.Weapons
{
    public class AmmoPackageProcessor : ListPackageProcessorBase<AmmoTableEntryPrimitive>
    {
        public AmmoPackageProcessor()
            : base(new ushort[] { 0x021D }, AmmoTableEntryPrimitive.Read)
        {
        }

        public override bool IsChunkFileMatching(string chunkFullFilename)
        {
            return chunkFullFilename == "/common/equip/shell_table.shl_tbl";
        }
    }
}
