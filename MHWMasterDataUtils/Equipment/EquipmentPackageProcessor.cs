using System;
using System.Collections.Generic;
using System.Text;

namespace MHWMasterDataUtils.Equipment
{
    public class EquipmentPackageProcessor : SimpleMapPackageProcessorBase<ushort, EquipmentPrimitive>
    {
        public EquipmentPackageProcessor()
            : base(0x005d, EquipmentPrimitive.Read, x => x.Id)
        {
        }

        public override bool IsChunkFileMatching(string chunkFullFilename)
        {
            return chunkFullFilename == "/common/equip/armor.am_dat";
        }
    }
}
