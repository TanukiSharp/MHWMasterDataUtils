using System;
using System.Collections.Generic;
using System.Text;

namespace MHWMasterDataUtils.Jewels
{
    public class JewelPackageProcessor : SimpleMapPackageProcessorBase<uint, JewelPrimitive>
    {
        public JewelPackageProcessor()
            : base(0x00AE, JewelPrimitive.Read, x => x.Id)
        {
        }

        public override bool IsChunkFileMatching(string chunkFullFilename)
        {
            return chunkFullFilename == "/common/item/skillGemParam.sgpa";
        }
    }
}
