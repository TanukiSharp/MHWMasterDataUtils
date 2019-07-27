using MHWMasterDataUtils.Builders;
using MHWMasterDataUtils.Builders.Weapons;
using MHWMasterDataUtils.Core;
using MHWMasterDataUtils.Crafting;
using MHWMasterDataUtils.Equipments;
using MHWMasterDataUtils.Items;
using MHWMasterDataUtils.Jewels;
using MHWMasterDataUtils.Languages;
using MHWMasterDataUtils.Sharpness;
using MHWMasterDataUtils.Skills;
using MHWMasterDataUtils.Weapons;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MHWMasterDataUtils.Tester
{
    class Program
    {
        static void Main()
        {
            new Program().Run();
        }

        private void Run()
        {
            Console.WriteLine("-=-=-=-=-=- START -=-=-=-=-=-");

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

            var armorCraft = new CraftPackageProcessor<ArmorClass>("/common/equip/armor.eq_crt");
            var weaponCraft = new CraftPackageProcessor<WeaponType>("/common/equip/weapon.eq_crt");

            var greatSwordLanguages = new LanguagePackageProcessor(WeaponsUtils.WeaponToLanguageFile(WeaponType.GreatSword));
            var hammerLanguages = new LanguagePackageProcessor(WeaponsUtils.WeaponToLanguageFile(WeaponType.Hammer));
            var dualBladeLanguages = new LanguagePackageProcessor(WeaponsUtils.WeaponToLanguageFile(WeaponType.DualBlades));
            var longSwordLanguages = new LanguagePackageProcessor(WeaponsUtils.WeaponToLanguageFile(WeaponType.LongSword));
            var huntingHornLanguages = new LanguagePackageProcessor(WeaponsUtils.WeaponToLanguageFile(WeaponType.HuntingHorn));
            var swordAndShieldLanguages = new LanguagePackageProcessor(WeaponsUtils.WeaponToLanguageFile(WeaponType.SwordAndShield));
            var lanceLanguages = new LanguagePackageProcessor(WeaponsUtils.WeaponToLanguageFile(WeaponType.Lance));
            var gunlanceLanguages = new LanguagePackageProcessor(WeaponsUtils.WeaponToLanguageFile(WeaponType.Gunlance));
            var switchAxeLanguages = new LanguagePackageProcessor(WeaponsUtils.WeaponToLanguageFile(WeaponType.SwitchAxe));
            var chargeBladeLanguages = new LanguagePackageProcessor(WeaponsUtils.WeaponToLanguageFile(WeaponType.ChargeBlade));
            var insectGlaiveLanguages = new LanguagePackageProcessor(WeaponsUtils.WeaponToLanguageFile(WeaponType.InsectGlaive));

            var weaponSeriesLanguages = new LanguagePackageProcessor("/common/text/steam/wep_series_\\w{3}.gmd");
            var steamItemsLanguages = new LanguagePackageProcessor("/common/text/steam/item_\\w{3}.gmd");
            var cmItemsLanguages = new LanguagePackageProcessor("/common/text/cm_item_\\w{3}.gmd");
            var itemsLanguages = new LanguagePackageProcessor("/common/text/item_\\w{3}.gmd");

            var skillLanguages = new LanguagePackageProcessor("/common/text/vfont/skill_pt_\\w{3}.gmd");
            var skillAbilitiesLanguages = new LanguagePackageProcessor("/common/text/vfont/skill_\\w{3}.gmd");

            var bowBootles = new BottleTablePackageProcessor();
            var weapons = new WeaponsPackageProcessor();
            var huntingHornNotes = new HuntingHornNotesPackageProcessor();
            var huntingHornSongs = new HuntingHornSongsPackageProcessor();
            var dualBladesSpecial = new DualBladesSpecialPackageProcessor();
            var axePhials = new AxePhialPackageProcessor();
            var gunlanceShells = new GunlanceShellPackageProcessor();

            var skills = new SkillsPackageProcessor();
            var skillAbilities = new SkillAbilitiesPackageProcessor();

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
                hammerLanguages,
                dualBladeLanguages,
                longSwordLanguages,
                huntingHornLanguages,
                swordAndShieldLanguages,
                lanceLanguages,
                gunlanceLanguages,
                switchAxeLanguages,
                chargeBladeLanguages,
                insectGlaiveLanguages,
                weaponSeriesLanguages,
                steamItemsLanguages,
                cmItemsLanguages,
                itemsLanguages,
                bowBootles,
                weapons,
                huntingHornNotes,
                huntingHornSongs,
                dualBladesSpecial,
                axePhials,
                gunlanceShells,
                skills,
                skillAbilities,
                skillLanguages,
                skillAbilitiesLanguages,
                new DumpPackageProcessor("/common/equip/rod_insect.rod_inse"),
            };

            using (var packageReader = new PackageReader(logger, fileProcessors))
                packageReader.Run(packagesFullPath);

            WeaponTreeName[] weaponTrees = new WeaponTreesBuilder(weaponSeriesLanguages, weapons).Build();
            SerializeJson(nameof(weaponTrees), weaponTrees);

            Skill[] skillEntries = new SkillsBuilder(
                skills,
                skillAbilities,
                skillLanguages,
                skillAbilitiesLanguages
            ).Build();
            SerializeJson("skills", skillEntries);

            Item[] highLevelItems = new ItemsBuilder(
                i => i.Type == ItemTypePrimitive.MonsterMaterial,
                items,
                steamItemsLanguages
            ).Build();
            SerializeJson("items", highLevelItems);

            Item[] jewelItems = new ItemsBuilder(
                i => i.Type == ItemTypePrimitive.Jewel,
                items,
                steamItemsLanguages
            ).Build();
            SerializeJson("jewels", jewelItems);

            WeaponBase[] dualBlades = new DualBladesWeaponBuilder(
                dualBladeLanguages,
                weapons,
                weaponCraft,
                weaponUpgrades,
                sharpness,
                dualBladesSpecial
            ).Build();
            SerializeJson("dual-blades", dualBlades);

            WeaponBase[] longSwords = new LongSwordWeaponBuilder(
                longSwordLanguages,
                weapons,
                weaponCraft,
                weaponUpgrades,
                sharpness
            ).Build();
            SerializeJson("long-swords", longSwords);

            WeaponBase[] swordAndShields = new SwordAndShieldWeaponBuilder(
                swordAndShieldLanguages,
                weapons,
                weaponCraft,
                weaponUpgrades,
                sharpness
            ).Build();
            SerializeJson("sword-and-shields", swordAndShields);

            WeaponBase[] greatSwords = new GreatSwordWeaponBuilder(
                greatSwordLanguages,
                weapons,
                weaponCraft,
                weaponUpgrades,
                sharpness
            ).Build();
            SerializeJson("great-swords", greatSwords);

            WeaponBase[] hammers = new HammerWeaponBuilder(
                hammerLanguages,
                weapons,
                weaponCraft,
                weaponUpgrades,
                sharpness
            ).Build();
            SerializeJson("hammers", hammers);

            WeaponBase[] huntingHorns = new HuntingHornWeaponBuilder(
                huntingHornLanguages,
                weapons,
                weaponCraft,
                weaponUpgrades,
                sharpness,
                huntingHornNotes,
                huntingHornSongs
            ).Build();
            SerializeJson("hunting-horns", huntingHorns);

            WeaponBase[] lances = new LanceWeaponBuilder(
                lanceLanguages,
                weapons,
                weaponCraft,
                weaponUpgrades,
                sharpness
            ).Build();
            SerializeJson("lances", lances);

            WeaponBase[] gunlances = new GunlanceWeaponBuilder(
                gunlanceLanguages,
                weapons,
                weaponCraft,
                weaponUpgrades,
                sharpness,
                gunlanceShells
            ).Build();
            SerializeJson("gunlances", gunlances);

            WeaponBase[] switchAxes = new AxeWeaponBuilder(
                WeaponType.SwitchAxe,
                switchAxeLanguages,
                weapons,
                weaponCraft,
                weaponUpgrades,
                sharpness,
                axePhials
            ).Build();
            SerializeJson("switch-axes", switchAxes);

            WeaponBase[] chargeBlades = new AxeWeaponBuilder(
                WeaponType.ChargeBlade,
                chargeBladeLanguages,
                weapons,
                weaponCraft,
                weaponUpgrades,
                sharpness,
                axePhials
            ).Build();
            SerializeJson("charge-blades", chargeBlades);

            WeaponBase[] insectGlaives = new InsectGlaiveWeaponBuilder(
                insectGlaiveLanguages,
                weapons,
                weaponCraft,
                weaponUpgrades,
                sharpness
            ).Build();
            SerializeJson("insect-glaives", insectGlaives);
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
