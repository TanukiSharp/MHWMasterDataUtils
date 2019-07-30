using System;
using System.IO;
using MHWMasterDataUtils.Armors;
using MHWMasterDataUtils.Builders;
using MHWMasterDataUtils.Builders.Equipment;
using MHWMasterDataUtils.Builders.Weapons;
using MHWMasterDataUtils.Core;
using MHWMasterDataUtils.Crafting;
using MHWMasterDataUtils.Items;
using MHWMasterDataUtils.Jewels;
using MHWMasterDataUtils.Languages;
using MHWMasterDataUtils.Sharpness;
using MHWMasterDataUtils.Skills;
using MHWMasterDataUtils.Weapons;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MHWMasterDataUtils.Tester
{
    class Program
    {
        static void Main()
        {
            new Program().Run();
        }

        private static string dataOutputFullPath;

        private void SetupDataOutputPath()
        {
            dataOutputFullPath = Lookup("MHWMasterDataUtils.sln");

            if (dataOutputFullPath == null)
                dataOutputFullPath = AppContext.BaseDirectory;

            dataOutputFullPath = Path.Join(dataOutputFullPath, "data");

            if (Directory.Exists(dataOutputFullPath) == false)
                Directory.CreateDirectory(dataOutputFullPath);
        }

        private void Run()
        {
            Console.WriteLine("-=-=-=-=-=- START -=-=-=-=-=-");

            string packagesFullPath = PackageUtility.GetPackagesFullPath();

            SetupDataOutputPath();

            ILogger logger = new ConsoleLogger(null, LogLevel.Debug);

            var DEBUG = new PrintFilenamePackageProcessor(logger);

            var equipmentUpgrades = new WeaponUpgradePackageProcessor("/common/equip/equip_custom.eq_cus");
            var insectUpgrades = new WeaponUpgradePackageProcessor("/common/equip/insect.eq_cus");
            var insectElemntUpgrades = new WeaponUpgradePackageProcessor("/common/equip/insect_element.eq_cus");
            var weaponUpgrades = new WeaponUpgradePackageProcessor("/common/equip/weapon.eq_cus");

            var jewels = new JewelPackageProcessor();
            var items = new ItemsPackageProcessor();
            var sharpness = new SharpnessPackageProcessor();

            var armorCraft = new CraftPackageProcessor<EquipmentType>("/common/equip/armor.eq_crt");
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
            var bowLanguages = new LanguagePackageProcessor(WeaponsUtils.WeaponToLanguageFile(WeaponType.Bow));
            var lightBowgunLanguages = new LanguagePackageProcessor(WeaponsUtils.WeaponToLanguageFile(WeaponType.LightBowgun));
            var heavyBowgunLanguages = new LanguagePackageProcessor(WeaponsUtils.WeaponToLanguageFile(WeaponType.HeavyBowgun));
            var armorLanguages = new LanguagePackageProcessor("/common/text/steam/armor_\\w{3}.gmd");
            var armorSeriesLanguages = new LanguagePackageProcessor("/common/text/steam/armor_series_\\w{3}.gmd");

            var weaponSeriesLanguages = new LanguagePackageProcessor("/common/text/steam/wep_series_\\w{3}.gmd");
            var steamItemsLanguages = new LanguagePackageProcessor("/common/text/steam/item_\\w{3}.gmd");
            var cmItemsLanguages = new LanguagePackageProcessor("/common/text/cm_item_\\w{3}.gmd");
            var itemsLanguages = new LanguagePackageProcessor("/common/text/item_\\w{3}.gmd");

            var skillLanguages = new LanguagePackageProcessor("/common/text/vfont/skill_pt_\\w{3}.gmd");
            var skillAbilitiesLanguages = new LanguagePackageProcessor("/common/text/vfont/skill_\\w{3}.gmd");

            var bowBottles = new BowBottleTablePackageProcessor();
            var weapons = new WeaponsPackageProcessor();
            var huntingHornNotes = new HuntingHornNotesPackageProcessor();
            var huntingHornSongs = new HuntingHornSongsPackageProcessor();
            var dualBladesSpecial = new DualBladesSpecialPackageProcessor();
            var switchAxePhials = new SwitchAxePhialPackageProcessor();
            var gunlanceShells = new GunlanceShellPackageProcessor();
            var ammos = new AmmoPackageProcessor();

            var skills = new SkillsPackageProcessor();
            var skillAbilities = new SkillAbilitiesPackageProcessor();

            var armors = new ArmorPackageProcessor();

            var fileProcessors = new IPackageProcessor[]
            {
                //DEBUG,
                equipmentUpgrades,
                insectUpgrades,
                insectElemntUpgrades,
                weaponUpgrades,
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
                bowLanguages,
                lightBowgunLanguages,
                heavyBowgunLanguages,
                weaponSeriesLanguages,
                steamItemsLanguages,
                cmItemsLanguages,
                itemsLanguages,
                armorLanguages,
                armorSeriesLanguages,
                bowBottles,
                weapons,
                huntingHornNotes,
                huntingHornSongs,
                dualBladesSpecial,
                switchAxePhials,
                gunlanceShells,
                ammos,
                skills,
                skillAbilities,
                skillLanguages,
                skillAbilitiesLanguages,
                armors
                //new DumpPackageProcessor("/common/equip/rod_insect.rod_inse"),
            };

            using (var packageReader = new PackageReader(logger, fileProcessors))
                packageReader.Run(packagesFullPath);

            WeaponTreeName[] weaponTrees = new WeaponTreeNameBuilder(weaponSeriesLanguages, weapons).Build();
            SerializeJson("weapon-trees", weaponTrees);

            Skill[] skillEntries = new SkillBuilder(
                skills,
                skillAbilities,
                skillLanguages,
                skillAbilitiesLanguages
            ).Build();
            SerializeJson("skills", skillEntries);

            Item[] highLevelItems = new ItemBuilder<Item>(
                i => i.Type == ItemTypePrimitive.MonsterMaterial,
                items,
                steamItemsLanguages
            ).Build();
            SerializeJson("items", highLevelItems);

            Jewel[] jewelItems = new JewelBuilder(
                items,
                steamItemsLanguages,
                jewels
            ).Build();
            SerializeJson("jewels", jewelItems);

            DualBlades[] dualBlades = new DualBladesWeaponBuilder(
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

            HuntingHorn[] huntingHorns = new HuntingHornWeaponBuilder(
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

            Gunlance[] gunlances = new GunlanceWeaponBuilder(
                gunlanceLanguages,
                weapons,
                weaponCraft,
                weaponUpgrades,
                sharpness,
                gunlanceShells
            ).Build();
            SerializeJson("gunlances", gunlances);

            SwitchAxe[] switchAxes = new SwitchAxeWeaponBuilder(
                switchAxeLanguages,
                weapons,
                weaponCraft,
                weaponUpgrades,
                sharpness,
                switchAxePhials
            ).Build();
            SerializeJson("switch-axes", switchAxes);

            ChargeBlade[] chargeBlades = new ChargeBladeWeaponBuilder(
                chargeBladeLanguages,
                weapons,
                weaponCraft,
                weaponUpgrades,
                sharpness
            ).Build();
            SerializeJson("charge-blades", chargeBlades);

            InsectGlaive[] insectGlaives = new InsectGlaiveWeaponBuilder(
                insectGlaiveLanguages,
                weapons,
                weaponCraft,
                weaponUpgrades,
                sharpness
            ).Build();
            SerializeJson("insect-glaives", insectGlaives);

            Bow[] bows = new BowWeaponBuilder(
                bowLanguages,
                weapons,
                weaponCraft,
                weaponUpgrades,
                bowBottles
            ).Build();
            SerializeJson("bows", bows);

            Bowgun[] lightBowguns = new BowgunWeaponBuilder(
                WeaponType.LightBowgun,
                lightBowgunLanguages,
                weapons,
                weaponCraft,
                weaponUpgrades,
                ammos
            ).Build();
            SerializeJson("light-bowguns", lightBowguns);

            Bowgun[] heavyBowguns = new BowgunWeaponBuilder(
                WeaponType.HeavyBowgun,
                heavyBowgunLanguages,
                weapons,
                weaponCraft,
                weaponUpgrades,
                ammos
            ).Build();
            SerializeJson("heavy-bowguns", heavyBowguns);

            ArmorPiece[] heads = new HeadEquipmentBuilder(
                armors,
                armorLanguages,
                armorSeriesLanguages
            ).Build();
            SerializeJson("heads", heads);

            Charm[] charms = new CharmEquipmentBuilder(
                armors,
                armorLanguages,
                armorSeriesLanguages
            ).Build();
            SerializeJson("charms", charms);
        }

        private static string Lookup(string filename)
        {
            string currentPath = AppContext.BaseDirectory.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

            while (true)
            {
                if (File.Exists(Path.Join(currentPath, filename)))
                    return currentPath;

                currentPath = Path.GetDirectoryName(currentPath);

                if (currentPath == null)
                    return null;
            }
        }

        private static void SerializeJson(string filename, object instance)
        {
            using (var sw = new StringWriter())
            {
                sw.NewLine = "\n";

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

                    File.WriteAllText(Path.Combine(dataOutputFullPath, $"{filename}.json"), sw.ToString());
                }
            }
        }
    }
}
