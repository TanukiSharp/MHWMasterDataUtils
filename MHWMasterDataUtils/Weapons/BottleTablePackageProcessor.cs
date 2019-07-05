using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MHWMasterDataUtils.Weapons.Primitives;

namespace MHWMasterDataUtils.Weapons
{
    public class BottleTablePackageProcessor : SimpleListPackageProcessorBase<BowBottlePrimitive>
    {
        public BottleTablePackageProcessor()
            : base(0x01A6, BowBottlePrimitive.Read)
        {
        }

        public override bool IsChunkFileMatching(string filename)
        {
            return filename == "/common/equip/bottle_table.bbtbl";
        }
    }
}
