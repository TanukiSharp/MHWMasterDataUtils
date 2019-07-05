using System;
using System.Collections.Generic;
using System.Text;

namespace MHWMasterDataUtils.Armors
{
    public class ArmorPackageProcessor : SimplePackageProcessorBase<ushort, ArmorPrimitive>
    {
        public ArmorPackageProcessor()
            : base(0x005d, ArmorPrimitive.Read, x => x.Id)
        {
        }

        public override bool IsChunkFileMatching(string chunkFullFilename)
        {
            return chunkFullFilename == "/common/equip/armor.am_dat";
        }
    }
}
