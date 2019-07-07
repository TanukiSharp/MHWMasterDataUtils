using System;
using System.Collections.Generic;
using System.Text;
using MHWMasterDataUtils.Weapons.Primitives;

namespace MHWMasterDataUtils.Weapons
{
    public class WeaponUpgradePackageProcessor : SimpleMapPackageProcessorBase<ushort, WeaponUpgradeEntryPrimitive>
    {
        public string MatchingChunkFullFilename { get; }

        public WeaponUpgradePackageProcessor(string matchingChunkFullFilename)
            : base(0x0051, WeaponUpgradeEntryPrimitive.Read, x => x.EquipId)
        {
            MatchingChunkFullFilename = matchingChunkFullFilename;
        }

        public override bool IsChunkFileMatching(string chunkFullFilename)
        {
            return chunkFullFilename == MatchingChunkFullFilename;
        }
    }
}
