using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MHWMasterDataUtils.Weapons.Primitives;

namespace MHWMasterDataUtils.Weapons
{
    public class BowBottleTablePackageProcessor : SimpleListPackageProcessorBase<BowBottleTableEntryPrimitive>
    {
        public BowBottleTablePackageProcessor()
            : base(0x01A6, BowBottleTableEntryPrimitive.Read)
        {
        }

        public override bool IsChunkFileMatching(string filename)
        {
            return filename == "/common/equip/bottle_table.bbtbl";
        }
    }
}
