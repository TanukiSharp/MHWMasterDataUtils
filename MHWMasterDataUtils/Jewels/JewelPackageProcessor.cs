using System;
using System.Collections.Generic;
using System.Text;

namespace MHWMasterDataUtils.Jewels
{
    public class JewelPackageProcessor : MapPackageProcessorBase<uint, JewelPrimitive>
    {
        public JewelPackageProcessor()
            : base(new ushort[] { 0x00BC }, JewelPrimitive.Read, x => x.Id)
        {
        }

        public override bool IsChunkFileMatching(string chunkFullFilename)
        {
            return chunkFullFilename == "/common/item/skillGemParam.sgpa";
        }
    }
}
