using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MHWMasterDataUtils.Core
{
    public class GunlanceShell
    {
        [JsonProperty("shellType")]
        public int ShellType { get; set; }
        [JsonProperty("shellLevel")]
        public int ShellLevel { get; set; }
    }
}
