using System;
using System.Collections.Generic;
using System.Text;

namespace MHWMasterDataUtils.Weapons.HighLevel
{
    public abstract class WeaponBase
    {
        public WeaponType WeaponType { get; }
        public uint Id { get; }
        public byte Color { get; }
        public byte NameId { get; }
        public byte TreeId { get; }

        protected WeaponBase(
            WeaponType weaponType,
            uint id,
            byte color,
            byte treeId
        )
        {
            WeaponType = weaponType;
            Id = id;
            Color = color;
            TreeId = treeId;
        }
    }
}
