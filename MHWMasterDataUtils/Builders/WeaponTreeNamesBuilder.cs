﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MHWMasterDataUtils.Equipments;
using MHWMasterDataUtils.Languages;
using MHWMasterDataUtils.Weapons;
using MHWMasterDataUtils.Weapons.HighLevel;
using MHWMasterDataUtils.Weapons.Primitives;
using Newtonsoft.Json;

namespace MHWMasterDataUtils.Builders
{
    public class WeaponTreeName
    {
        [JsonProperty("treeId")]
        public int TreeId { get; set; }
        [JsonProperty("name")]
        public Dictionary<string, string> Name { get; set; }
    }

    public class WeaponTreesBuilder
    {
        private readonly LanguagePackageProcessor weaponSeriesLanguages;
        private readonly WeaponsPackageProcessor weapons;

        public WeaponTreesBuilder(
            LanguagePackageProcessor weaponSeriesLanguages,
            WeaponsPackageProcessor weapons
        )
        {
            this.weaponSeriesLanguages = weaponSeriesLanguages;
            this.weapons = weapons;
        }

        public WeaponTreeName[] Build()
        {
            var treeNames = new Dictionary<byte, WeaponTreeName>();

            foreach (WeaponClass weaponClass in Enum.GetValues(typeof(WeaponClass)))
            {
                if (weaponClass == WeaponClass.None)
                    continue;

                foreach (KeyValuePair<uint, WeaponPrimitiveBase> weapon in weapons.Table[weaponClass])
                {
                    byte treeId = weapon.Value.TreeId;
                    bool isDisabledTree = false;
                    var name = new Dictionary<string, string>();

                    if (treeNames.ContainsKey(treeId))
                        continue;

                    foreach (LanguageIdPrimitive language in LanguageUtils.Languages)
                    {
                        if (weaponSeriesLanguages.Table[language].TryGetValue(treeId, out LanguageItem treeSeriesLanguageItem) == false)
                            continue;

                        string treeName = treeSeriesLanguageItem.Value;

                        if (treeName == "無効" || treeName == string.Empty)
                        {
                            isDisabledTree = true;
                            break;
                        }

                        name.Add(LanguageUtils.LanguageToStringCode(language), treeName);
                    }

                    if (isDisabledTree)
                        continue;

                    treeNames.Add(treeId, new WeaponTreeName { TreeId = treeId, Name = name });
                }
            }

            return treeNames.Values.OrderBy(x => x.TreeId).ToArray();
        }
    }
}