using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MHWMasterDataUtils.Items;
using MHWMasterDataUtils.Languages;

using core = MHWMasterDataUtils.Core;

namespace MHWMasterDataUtils.Builders
{
    public class ItemsBuilder
    {
        private readonly Predicate<ItemEntryPrimitive> filter;
        private readonly ItemsPackageProcessor items;
        private readonly LanguagePackageProcessor steamItemsLanguages;

        public ItemsBuilder(
            Predicate<ItemEntryPrimitive> filter,
            ItemsPackageProcessor items,
            LanguagePackageProcessor steamItemsLanguages
        )
        {
            this.filter = filter;
            this.items = items;
            this.steamItemsLanguages = steamItemsLanguages;
        }

        public core.Item[] Build()
        {
            var result = new List<core.Item>();

            foreach (ItemEntryPrimitive itemEntry in items.Table.Values.OrderBy(x => x.SortOrder))
            {
                uint nameIndex = itemEntry.Id * 2;
                uint descriptionIndex = nameIndex + 1;

                if (LanguageUtils.IsValidText(steamItemsLanguages.Table, nameIndex) == false ||
                    LanguageUtils.IsValidText(steamItemsLanguages.Table, descriptionIndex) == false)
                    continue;

                if (filter != null && filter(itemEntry) == false)
                    continue;

                Dictionary<string, string> name = LanguageUtils.CreateLocalizations(steamItemsLanguages.Table, nameIndex);
                Dictionary<string, string> description = LanguageUtils.CreateLocalizations(steamItemsLanguages.Table, descriptionIndex, true);

                result.Add(new core.Item
                {
                    Id = itemEntry.Id,
                    Name = name,
                    Description = description,
                    Rarity = itemEntry.Rarity,
                    CarryLimit = itemEntry.CarryLimit,
                    BuyPrice = itemEntry.BuyPrice,
                    SellPrice = itemEntry.SellPrice,
                });
            }

            return result.ToArray();
        }
    }
}
