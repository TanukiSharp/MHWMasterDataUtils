using MHWMasterDataUtils.Builders;
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
        static async Task Main()
        {
            await new Program().Run().ConfigureAwait(false);
        }

        private async Task Run()
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
                new DumpPackageProcessor("/common/equip/wep_glan.wep_glan"),
            };

            using (var packageReader = new PackageReader(logger, fileProcessors))
                await packageReader.Run(packagesFullPath).ConfigureAwait(false);

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

            SharpnessWeapon[] dualBlades = new SharpnessWeaponBuilder(
                WeaponType.DualBlades,
                dualBladeLanguages,
                sharpness,
                weapons,
                weaponCraft,
                weaponUpgrades,
                null,
                null,
                dualBladesSpecial,
                null,
                null
            ).Build();
            SerializeJson("dual-blades", dualBlades);

            SharpnessWeapon[] longSwords = new SharpnessWeaponBuilder(
                WeaponType.LongSword,
                longSwordLanguages,
                sharpness,
                weapons,
                weaponCraft,
                weaponUpgrades,
                null,
                null,
                null,
                null,
                null
            ).Build();
            SerializeJson("long-swords", longSwords);

            SharpnessWeapon[] swordAndShields = new SharpnessWeaponBuilder(
                WeaponType.SwordAndShield,
                swordAndShieldLanguages,
                sharpness,
                weapons,
                weaponCraft,
                weaponUpgrades,
                null,
                null,
                null,
                null,
                null
            ).Build();
            SerializeJson("sword-and-shields", swordAndShields);

            SharpnessWeapon[] greatSwords = new SharpnessWeaponBuilder(
                WeaponType.GreatSword,
                greatSwordLanguages,
                sharpness,
                weapons,
                weaponCraft,
                weaponUpgrades,
                null,
                null,
                null,
                null,
                null
            ).Build();
            SerializeJson("great-swords", greatSwords);

            SharpnessWeapon[] hammers = new SharpnessWeaponBuilder(
                WeaponType.Hammer,
                hammerLanguages,
                sharpness,
                weapons,
                weaponCraft,
                weaponUpgrades,
                null,
                null,
                null,
                null,
                null
            ).Build();
            SerializeJson("hammers", hammers);

            SharpnessWeapon[] huntingHorns = new SharpnessWeaponBuilder(
                WeaponType.HuntingHorn,
                huntingHornLanguages,
                sharpness,
                weapons,
                weaponCraft,
                weaponUpgrades,
                huntingHornNotes,
                huntingHornSongs,
                null,
                null,
                null
            ).Build();
            SerializeJson("hunting-horns", huntingHorns);

            SharpnessWeapon[] lances = new SharpnessWeaponBuilder(
                WeaponType.Lance,
                lanceLanguages,
                sharpness,
                weapons,
                weaponCraft,
                weaponUpgrades,
                null,
                null,
                null,
                null,
                null
            ).Build();
            SerializeJson("lances", lances);

            SharpnessWeapon[] gunlances = new SharpnessWeaponBuilder(
                WeaponType.Gunlance,
                gunlanceLanguages,
                sharpness,
                weapons,
                weaponCraft,
                weaponUpgrades,
                null,
                null,
                null,
                null,
                gunlanceShells
            ).Build();
            SerializeJson("gunlances", gunlances);

            SharpnessWeapon[] switchAxes = new SharpnessWeaponBuilder(
                WeaponType.SwitchAxe,
                switchAxeLanguages,
                sharpness,
                weapons,
                weaponCraft,
                weaponUpgrades,
                null,
                null,
                null,
                axePhials,
                null
            ).Build();
            SerializeJson("switch-axes", switchAxes);

            SharpnessWeapon[] chargeBlades = new SharpnessWeaponBuilder(
                WeaponType.ChargeBlade,
                chargeBladeLanguages,
                sharpness,
                weapons,
                weaponCraft,
                weaponUpgrades,
                null,
                null,
                null,
                axePhials,
                null
            ).Build();
            SerializeJson("charge-blades", chargeBlades);

            SharpnessWeapon[] insectGlaives = new SharpnessWeaponBuilder(
                WeaponType.InsectGlaive,
                insectGlaiveLanguages,
                sharpness,
                weapons,
                weaponCraft,
                weaponUpgrades,
                null,
                null,
                null,
                null,
                null
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

                    serializer.Converters.Add(new HuntingHornSongPrimitiveConverter());

                    serializer.Serialize(jw, instance);

                    File.WriteAllText(Path.Combine(AppContext.BaseDirectory, $"{filename}.json"), sw.ToString());
                }
            }
        }
    }

    public class HuntingHornSongPrimitiveConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Weapons.Primitives.HuntingHornSongPrimitive);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (writer == null)
                throw new ArgumentNullException(nameof(writer));
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            var v = (Weapons.Primitives.HuntingHornSongPrimitive)value;

            writer.WriteStartObject();

            writer.WritePropertyName("effect");
            writer.WriteValue(v.Effect.ToString());

            writer.WritePropertyName("notes");

            writer.WriteStartArray();

            if (v.Note1 != HuntingHornNoteColor.Disabled)
                writer.WriteValue(v.Note1.ToString().ToLower());
            if (v.Note2 != HuntingHornNoteColor.Disabled)
                writer.WriteValue(v.Note2.ToString().ToLower());
            if (v.Note3 != HuntingHornNoteColor.Disabled)
                writer.WriteValue(v.Note3.ToString().ToLower());
            if (v.Note4 != HuntingHornNoteColor.Disabled)
                writer.WriteValue(v.Note4.ToString().ToLower());
            
            writer.WriteEndArray();

            writer.WriteEndObject();
        }
    }
}
