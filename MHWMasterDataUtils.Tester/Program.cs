using MHWMasterDataUtils.Languages;
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
            if (filename.StartsWith(@"\common\text\steam\") == false || filename.EndsWith(".gmd") == false)
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

            var greatSwordLanguages = new LanguagePackageFileProcessor(x => x == @"\common\text\vfont\l_sword_eng.gmd");

            var fileProcessors = new IPackageProcessor[]
            {
                new PrintFilenamePackageProcessor(logger),
                //new LanguagePackageFileProcessor(IsWeaponLa nguageFile),
                new LanguagePackageFileProcessor(x => x == @"\common\text\steam\wep_series_eng.gmd"),
                greatSwordLanguages,
                new BottleTablePackageFileProcessor(),
                new WeaponsPackageFileProcessor(),
            };

            await PackageUtility.RunPackageProcessors(logger, packagesFullPath, fileProcessors);
        }
    }
}
