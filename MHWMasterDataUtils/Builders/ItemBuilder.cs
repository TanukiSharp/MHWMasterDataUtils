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

        private static readonly (uint targetId, uint sourceId)[] NameReplacementTable =
        {
            (1638, 4540),
        };

        private static readonly (uint targetId, uint sourceId)[] DescriptionReplacementTable =
        {
            (1639, 4541),
        };

        public T[] Build()
        {
            var result = new List<T>();

            foreach (ItemEntryPrimitive itemEntry in items.Table.Values.OrderBy(x => x.SortOrder))
            {
                uint nameIndex = itemEntry.Id * 2;
                uint descriptionIndex = nameIndex + 1;

                if (NameReplacementTable.Any(x => x.sourceId == nameIndex))
                    continue;

                (uint targetId, uint sourceId) = NameReplacementTable.FirstOrDefault(x => x.targetId == nameIndex);
                if (targetId != 0 && sourceId != 0 && targetId == nameIndex)
                    nameIndex = sourceId;

                if (DescriptionReplacementTable.Any(x => x.sourceId == descriptionIndex))
                    continue;

                (targetId, sourceId) = DescriptionReplacementTable.FirstOrDefault(x => x.targetId == descriptionIndex);
                if (targetId != 0 && sourceId != 0 && targetId == descriptionIndex)
                    descriptionIndex = sourceId;

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
