using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MHWMasterDataUtils.Core
{
    public class MeleeWeapon : WeaponBase
    {
        [JsonProperty("sharpness"), JsonConverter(typeof(SharpnessJsonConverter))]
        public SharpnessInfo[] Sharpness { get; set; }
        [JsonProperty("weaponSpecific")]
        public object WeaponSpecific { get; set; }

        public override string ToString()
        {
            var text = new StringBuilder($"[{Id}] {Name[LanguageUtils.DefaultLanguageCode]}");

            if (WeaponSpecific is GunlanceShell shell)
                text.Append($" {(GunlanceShellType)shell.ShellType}-{shell.ShellLevel}");

            text.Append($" (parent: {ParentId})");

            return text.ToString();
        }
    }
}
