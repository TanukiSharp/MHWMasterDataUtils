using System;
using System.Collections.Generic;
using System.Text;

namespace MHWMasterDataUtils.Equipment
{
    public class EquipmentPackageProcessor : MapPackageProcessorBase<uint, EquipmentPrimitive>
    {
        public EquipmentPackageProcessor()
            : base(new ushort[] { 0x005F }, EquipmentPrimitive.Read, x => x.Id)
        {
        }

        public override bool IsChunkFileMatching(string chunkFullFilename)
        {
            return chunkFullFilename == "/common/equip/armor.am_dat";
        }
    }
}
