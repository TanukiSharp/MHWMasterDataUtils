using System;
using System.Collections.Generic;
using System.Text;
using MHWMasterDataUtils.Languages;

using core = MHWMasterDataUtils.Core;

namespace MHWMasterDataUtils.Builders
{
    public class ArmorSeriesBuilder
    {
        private readonly LanguagePackageProcessor armorSeriesLanguages;

        public ArmorSeriesBuilder(LanguagePackageProcessor armorSeriesLanguages)
        {
            this.armorSeriesLanguages = armorSeriesLanguages;
        }

        private static readonly LanguageUtils.LanguageValueProcessor[] translationValueProcessors = new[]
        {
            LanguageUtils.ReplaceLineFeedWithSpace,
            LanguageUtils.ReplaceAlphaSymbol,
            LanguageUtils.ReplaceBetaSymbol,
            LanguageUtils.ReplaceGammaSymbol,
        };

        public core.ArmorSeries[] Build()
        {
            var result = new List<core.ArmorSeries>();

            foreach (KeyValuePair<uint, LanguageItem> x in armorSeriesLanguages.Table[LanguageIdPrimitive.English])
            {
                LanguageItem item = x.Value;

                if (LanguageUtils.IsValidText(armorSeriesLanguages.Table, x.Key) == false)
                    continue;

                result.Add(new core.ArmorSeries
                {
                    Id = (int)x.Key,
                    Name = LanguageUtils.CreateLocalizations(armorSeriesLanguages.Table, x.Key, translationValueProcessors)
                });
            }

            return result.ToArray();
        }
    }
}
