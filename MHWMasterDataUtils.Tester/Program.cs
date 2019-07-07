using MHWMasterDataUtils.Builders;
using MHWMasterDataUtils.Crafting;
using MHWMasterDataUtils.Equipments;
using MHWMasterDataUtils.Items;
using MHWMasterDataUtils.Jewels;
using MHWMasterDataUtils.Languages;
using MHWMasterDataUtils.Sharpness;
using MHWMasterDataUtils.Weapons;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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

        private async Task Run(string[] args)
        {
            string packagesFullPath = PackageUtility.GetPackagesFullPath();

            ILogger logger = new ConsoleLogger(null, LogLevel.Debug);

            var DEBUG = new PrintFilenamePackageProcessor(logger);

            var equipmentUpgrades = new WeaponUpgradePackageProcessor("/common/equip/equip_custom.eq_cus");
            var insectUpgrades = new WeaponUpgradePackageProcessor("/common/equip/insect.eq_cus");
            var insectElemntUpgrades = new WeaponUpgradePackageProcessor("/common/equip/insect_element.eq_cus");
            var weaponUpgrades = new WeaponUpgradePackageProcessor("/common/equip/weapon.eq_cus");

            var armors = new AmmoPackageProcessor();
            var jewels = new JewelPackageProcessor();
            var items = new ItemsPackageProcessor();
            var sharpness = new SharpnessPackageProcessor();

            var armorCraft = new CraftPackageProcessor("/common/equip/armor.eq_crt");
            var weaponCraft = new CraftPackageProcessor("/common/equip/weapon.eq_crt");

            var greatSwordLanguages = new LanguagePackageProcessor("/common/text/vfont/l_sword_\\w{3}.gmd");

            var weaponSeriesLanguages = new LanguagePackageProcessor("/common/text/steam/wep_series_\\w{3}.gmd");
            var steamItemsLanguages = new LanguagePackageProcessor("/common/text/steam/item_\\w{3}.gmd");
            var cmItemsLanguages = new LanguagePackageProcessor("/common/text/cm_item_\\w{3}.gmd");
            var itemsLanguages = new LanguagePackageProcessor("/common/text/item_\\w{3}.gmd");
            var vfontItemsLanguaegs = new LanguagePackageProcessor("/common/text/vfont/item_\\w{3}.gmd");
            var skillsLanguages = new LanguagePackageProcessor("/common/text/a_skill_\\{3}.gmd");
            var vfontSkillsLanguages = new LanguagePackageProcessor("/common/text/vfont/skill_\\w{3}.gmd");
            var vfontSkillsPtLanguages = new LanguagePackageProcessor("/common/text/vfont/skill_pt_\\{3}.gmd");

            var bowBootles = new BottleTablePackageProcessor();
            var weapons = new WeaponsPackageProcessor();

            var fileProcessors = new IPackageProcessor[]
            {
                DEBUG,
                equipmentUpgrades,
                insectUpgrades,
                insectElemntUpgrades,
                weaponUpgrades,
                armors,
                jewels,
                items,
                sharpness,
                armorCraft,
                weaponCraft,
                greatSwordLanguages,
                weaponSeriesLanguages,
                steamItemsLanguages,
                cmItemsLanguages,
                itemsLanguages,
                vfontItemsLanguaegs,
                skillsLanguages,
                vfontSkillsLanguages,
                vfontSkillsPtLanguages,
                bowBootles,
                weapons,
            };

            var packageReader = new PackageReader(logger, fileProcessors);
            await packageReader.Run(packagesFullPath);

            WeaponTreeName[] weaponTrees = new WeaponTreesBuilder(weaponSeriesLanguages, weapons).Build();
            SerializeJson(nameof(weaponTrees), weaponTrees);

            new SharpnessWeaponsBuilder(
                WeaponClass.GreatSword,
                equipmentUpgrades,
                weaponCraft,
                weaponUpgrades,
                items,
                sharpness,
                greatSwordLanguages,
                weaponSeriesLanguages,
                weapons
            ).Build();
        }

        private static void SerializeJson(string filename, object instance)
        {
            using (var sw = new StringWriter())
            {
                using (var jw = new JsonTextWriter(sw))
                {
                    jw.Formatting = Formatting.Indented;
                    jw.IndentChar = ' ';
                    jw.Indentation = 4;

                    var serializer = new JsonSerializer
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    };
                    serializer.Serialize(jw, instance);

                    File.WriteAllText(Path.Combine(AppContext.BaseDirectory, $"{filename}.json"), sw.ToString());
                }
            }
        }
    }
}
