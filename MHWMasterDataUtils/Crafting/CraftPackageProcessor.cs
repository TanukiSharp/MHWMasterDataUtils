using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MHWMasterDataUtils.Crafting
{
    public class CraftPackageProcessor : SimpleMapPackageProcessorBase<ushort, CraftEntryPrimitive>
    {
        private readonly IEnumerable<string> matchingChunkFullFilenames;

        public override bool IsChunkFileMatching(string chunkFullFilename)
        {
            foreach (string matchingChunkFullFilename in matchingChunkFullFilenames)
            {
                if (string.Equals(matchingChunkFullFilename, chunkFullFilename, StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            return false;
        }

        public CraftPackageProcessor(string matchingChunkFullFilename)
            : this(new[] { matchingChunkFullFilename })
        {
        }

        public CraftPackageProcessor(IEnumerable<string> matchingChunkFullFilenames)
            : base(0x0051, CraftEntryPrimitive.Read, x => x.EquipId)
        {
            this.matchingChunkFullFilenames = matchingChunkFullFilenames;
        }

    }
}
