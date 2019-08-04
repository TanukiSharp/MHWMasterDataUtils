using System;
using System.Collections.Generic;
using System.Text;

namespace MHWMasterDataUtils.Skills
{
    public class SkillsPackageProcessor : ListPackageProcessorBase<SkillPrimitive>
    {
        public SkillsPackageProcessor()
            : base(0x005e, SkillPrimitive.Read)
        {
        }

        public override bool IsChunkFileMatching(string chunkFullFilename)
        {
            return chunkFullFilename == "/common/equip/skill_point_data.skl_pt_dat";
        }
    }
}
