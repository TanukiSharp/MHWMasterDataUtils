using System;
using System.Collections.Generic;
using System.Text;
using MHWMasterDataUtils.Core;

namespace MHWMasterDataUtils.Weapons.Primitives
{
    public class GunlanceShellPrimitive
    {
        public readonly ushort Id;
        public readonly GunlanceShellType ShellType;
        public readonly ushort ShellLevel;

        private GunlanceShellPrimitive(ushort id, GunlanceShellType shellType, ushort shellLevel)
        {
            Id = id;
            ShellType = shellType;
            ShellLevel = shellLevel;
        }

        public static GunlanceShellPrimitive Read(Reader reader)
        {
            ushort id = reader.ReadUInt16();
            reader.Offset(2);
            var shellType = (GunlanceShellType)reader.ReadUInt16();
            ushort shellLevel = reader.ReadUInt16();

            return new GunlanceShellPrimitive(id, shellType, shellLevel);
        }
    }
}
