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

        public override string ToString()
        {
            var text = new StringBuilder($"[{Id}] {Name[LanguageUtils.DefaultLanguageCode]}");

            if (this is Gunlance gunlance)
                text.Append($" {(GunlanceShellType)gunlance.Shelling.ShellType}-{gunlance.Shelling.ShellLevel}");

            text.Append($" (parent: {ParentId})");

            return text.ToString();
        }
    }
}
