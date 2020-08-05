using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MHWMasterDataUtils.Items;
using MHWMasterDataUtils.Languages;

using core = MHWMasterDataUtils.Core;

namespace MHWMasterDataUtils.Builders
{
    public class ItemBuilder<T> where T : core.Item, new()
    {
        private readonly Predicate<ItemEntryPrimitive> filter;
        private readonly ItemsPackageProcessor items;
        private readonly LanguagePackageProcessor steamItemsLanguages;

        public ItemBuilder(
            Predicate<ItemEntryPrimitive> filter,
            ItemsPackageProcessor items,
            LanguagePackageProcessor steamItemsLanguages
        )
        {
            this.filter = filter;
            this.items = items;
            this.steamItemsLanguages = steamItemsLanguages;
        }

        protected virtual T CreateItemInstance()
        {
            return new T();
        }

        protected virtual bool UpdateItem(T item)
        {
            return true;
        }

        private static readonly LanguageUtils.LanguageValueProcessor[] languageValueProcessors = new[]
        {
            LanguageUtils.ReplaceLineFeedWithSpace,
            LanguageUtils.StyleTextRemover
        };

        public T[] Build()
        {
            var result = new List<T>();

            foreach (ItemEntryPrimitive itemEntry in items.Table.Values.OrderBy(x => x.SortOrder))
            {
                uint nameIndex = itemEntry.Id * 2;
                uint descriptionIndex = nameIndex + 1;

                if (LanguageUtils.IsValidText(steamItemsLanguages.Table, nameIndex) == false ||
                    LanguageUtils.IsValidText(steamItemsLanguages.Table, descriptionIndex) == false)
                    continue;

                if (filter != null && filter(itemEntry) == false)
                    continue;

                Dictionary<string, string> name = LanguageUtils.CreateLocalizations(steamItemsLanguages.Table, nameIndex, languageValueProcessors);
                Dictionary<string, string> description = LanguageUtils.CreateLocalizations(steamItemsLanguages.Table, descriptionIndex, languageValueProcessors);

                T item = CreateItemInstance();

                item.Id = itemEntry.Id;
                item.Name = name;
                item.Description = description;
                item.Rarity = itemEntry.Rarity;
                item.CarryLimit = itemEntry.CarryLimit;
                item.BuyPrice = itemEntry.BuyPrice;
                item.SellPrice = itemEntry.SellPrice;

                if (UpdateItem(item))
                    result.Add(item);
            }

            return result.ToArray();
        }
    }
}
