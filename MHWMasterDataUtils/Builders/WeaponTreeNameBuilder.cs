﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MHWMasterDataUtils.Languages;
using MHWMasterDataUtils.Weapons;

using core = MHWMasterDataUtils.Core;

namespace MHWMasterDataUtils.Builders
{
    public class WeaponTreeNameBuilder
    {
        private readonly LanguagePackageProcessor weaponSeriesLanguages;
        private readonly WeaponsPackageProcessor weapons;

        public WeaponTreeNameBuilder(
            LanguagePackageProcessor weaponSeriesLanguages,
            WeaponsPackageProcessor weapons
        )
        {
            this.weaponSeriesLanguages = weaponSeriesLanguages;
            this.weapons = weapons;
        }

        public core.WeaponTreeName[] Build()
        {
            var treeNames = new Dictionary<byte, core.WeaponTreeName>();

            foreach (core.WeaponType weaponClass in Enum.GetValues(typeof(core.WeaponType)))
            {
                if (weaponClass == core.WeaponType.None)
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
                        Dictionary<uint, LanguageItem> languageEntries;
#if DEBUG
                        // Allow to work with degraded data in debug mode.
                        if (weaponSeriesLanguages.Table.TryGetValue(language, out languageEntries) == false)
                            continue;
#else
                        languageEntries = weaponSeriesLanguages.Table[language];
#endif
                        if (languageEntries.TryGetValue(treeId, out LanguageItem treeSeriesLanguageItem) == false)
                            continue;

                        string treeName = treeSeriesLanguageItem.Value;

                        if (LanguageUtils.IsValidText(treeName) == false)
                        {
                            isDisabledTree = true;
                            break;
                        }

                        if (treeName == "無効" || treeName.Length == 0)
                        {
                            isDisabledTree = true;
                            break;
                        }

                        name.Add(LanguageUtils.LanguageIdToLanguageCode(language), treeName);
                    }

                    if (isDisabledTree)
                        continue;

                    treeNames.Add(treeId, new core.WeaponTreeName { TreeId = treeId, Name = name });
                }
            }

            return treeNames.Values.OrderBy(x => x.TreeId).ToArray();
        }
    }
}
