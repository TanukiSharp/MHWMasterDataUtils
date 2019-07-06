using MHWMasterDataUtils.Crafting;
using MHWMasterDataUtils.Items;
using MHWMasterDataUtils.Jewels;
using MHWMasterDataUtils.Languages;
using MHWMasterDataUtils.Sharpness;
using MHWMasterDataUtils.Weapons;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MHWMasterDataUtils.Tester
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await new Program().Run(args);
        }

        private static readonly string[] WeaponFilenames = new string[]
        {
            "c_axe",
            "g_lance",
            "hammer",
            "l_sword",
            "lance",
            "rod",
            "s_axe",
            "sword",
            "tachi",
            "w_sword",
            "whistle",
            "lbg",
            "hbg",
            "bow"
        };

        private static bool IsWeaponLanguageFile(string filename)
        {
            if (filename.StartsWith("/common/text/steam/") == false || filename.EndsWith(".gmd") == false)
                return false;

            filename = Path.GetFileNameWithoutExtension(filename);

            int lastIndexOf = filename.LastIndexOf('_');
            if (lastIndexOf < 0)
                return false;

            foreach (string weaponFilename in WeaponFilenames)
            {
                if (lastIndexOf == weaponFilename.Length && string.CompareOrdinal(weaponFilename, 0, filename, 0, weaponFilename.Length) == 0)
                    return true;
            }

            return false;
        }

        private async Task Run(string[] args)
        {
            string packagesFullPath = PackageUtility.GetPackagesFullPath();

            ILogger logger = new ConsoleLogger(null, LogLevel.Debug);

            var greatSwordLanguages = new LanguagePackageProcessor("vfont/l_sword_eng", x => x == "/common/text/vfont/l_sword_eng.gmd");

            var fileProcessors = new IPackageProcessor[]
            {
                new AmmoPackageProcessor(),

                new JewelPackageProcessor(),

                new CraftPackageProcessor("/common/equip/armor.eq_crt"),
                new CraftPackageProcessor("/common/equip/weapon.eq_crt"),
                new CraftPackageProcessor("/common/equip/ot_equip.eq_crt"),
                new CraftPackageProcessor("/common/equip/equip_custom.eq_cus"),
                new CraftPackageProcessor("/common/equip/weapon.eq_cus"),

                new ItemsPackageProcessor(),
                new PrintFilenamePackageProcessor(logger),
                new SharpnessPackageProcessor(),
                //new LanguagePackageProcessor(IsWeaponLanguageFile),
                new LanguagePackageProcessor("wep_series_eng", x => x == "/common/text/steam/wep_series_eng.gmd"),
                greatSwordLanguages,
                new LanguagePackageProcessor("steam/item_eng", x => x == "/common/text/steam/item_eng.gmd"),
                new LanguagePackageProcessor("cm_item_eng", x => x == "/common/text/cm_item_eng.gmd"),
                new LanguagePackageProcessor("item_eng", x => x == "/common/text/item_eng.gmd"),
                new LanguagePackageProcessor("vfont/item_eng", x => x == "/common/text/vfont/item_eng.gmd"),
                new LanguagePackageProcessor("a_skill_eng", x => x == "/common/text/a_skill_eng.gmd"),

                new LanguagePackageProcessor("vfont/skill_eng", x => x == "/common/text/vfont/skill_eng.gmd"),
                new LanguagePackageProcessor("vfont/skill_pt_eng", x => x == "/common/text/vfont/skill_pt_eng.gmd"),

                new BottleTablePackageProcessor(),
                new WeaponsPackageProcessor(),
            };

            var packageReader = new PackageReader(logger, fileProcessors);
            await packageReader.Run(packagesFullPath);
        }
    }
}
