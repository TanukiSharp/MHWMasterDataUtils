using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MHWMasterDataUtils.Items
{
    public class ItemsPackageProcessor : SimplePackageProcessorBase<uint, ItemEntryPrimitive>
    {
        public override bool IsChunkFileMatching(string chunkFullFilename)
        {
            return chunkFullFilename == @"\common\item\itemData.itm";
        }

        public ItemsPackageProcessor()
            : base(0x00AE, ItemEntryPrimitive.Read, x => x.id)
        {
        }
    }
}
