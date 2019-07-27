using System;
using System.Collections.Generic;
using System.Text;
using MHWMasterDataUtils.Core;

namespace MHWMasterDataUtils.Builders.Weapons
{
    public struct WeaponComputedArguments
    {
        public readonly int ParentId;
        public readonly Dictionary<string, string> Name;
        public readonly Dictionary<string, string> Description;
        public readonly bool CanDowngrade;
        public readonly Craft Craft;

        public WeaponComputedArguments(
            int parentId,
            Dictionary<string, string> name,
            Dictionary<string, string> description,
            bool canDowngrade,
            Craft craft
        )
        {
            ParentId = parentId;
            Name = name;
            Description = description;
            CanDowngrade = canDowngrade;
            Craft = craft;
        }
    }
}
