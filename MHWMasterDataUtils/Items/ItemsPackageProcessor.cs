using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MHWMasterDataUtils.Items
{
    public class ItemsPackageProcessor : MapPackageProcessorBase<uint, ItemEntryPrimitive>
    {
        public override bool IsChunkFileMatching(string chunkFullFilename)
        {
            return chunkFullFilename == "/common/item/itemData.itm";
        }

        public ItemsPackageProcessor()
            : base(new ushort[] { 0x00BE }, new ushort[] { 0x00BD }, ItemEntryPrimitive.Read, x => x.Id)
        {
        }
    }
}
