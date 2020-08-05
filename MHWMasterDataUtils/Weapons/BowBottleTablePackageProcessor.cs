using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MHWMasterDataUtils.Weapons
{
    public class BowBottleTablePackageProcessor : ListPackageProcessorBase<BowBottleTableEntryPrimitive>
    {
        public BowBottleTablePackageProcessor()
            : base(new ushort[] { 0x021D }, BowBottleTableEntryPrimitive.Read)
        {
        }

        public override bool IsChunkFileMatching(string filename)
        {
            return filename == "/common/equip/bottle_table.bbtbl";
        }
    }
}
