using System;
using System.Collections.Generic;
using System.Text;

namespace MHWMasterDataUtils.Items
{
    [Flags]
    public enum ItemFlagsPrimitive : uint
    {
        Default = 1 << 0, // (net, fishing rod, basic ammo, etc)
        EZ = 1 << 1, // doesn't leave quest
        Unknown1 = 1 << 2,
        Usable = 1 << 3, // Potions, Meat, etc
        Appraisal = 1 << 4, // streamstones, etc
        Unknown2 = 1 << 5,
        MegaTag = 1 << 6, // star added to the icon
        Level1 = 1 << 7,
        Level2 = 1 << 8,
        Level3 = 1 << 9,
        GlitterEffect = 1 << 10, // ie: on gems
        Deliverable = 1 << 11, // eggs, lump of meat
        DoesNotShowInPouch = 1 << 12 // ?
    }
}
