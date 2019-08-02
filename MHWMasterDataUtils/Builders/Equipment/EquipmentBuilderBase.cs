using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MHWMasterDataUtils.Equipment;
using MHWMasterDataUtils.Languages;

using core = MHWMasterDataUtils.Core;

namespace MHWMasterDataUtils.Builders.Equipment
{
    public abstract class EquipmentBuilderBase<TEquiment> where TEquiment : core.EquipmentBase, new()
    {
        private readonly core.EquipmentType equipmentType;
        private readonly Predicate<EquipmentPrimitive> filter;
        private readonly EquipmentPackageProcessor equipments;
        private readonly LanguagePackageProcessor equipmentLanguages;
        private readonly Dictionary<ushort, EquipmentUpgradeEntryPrimitive> equipmentUpgrades;
        private readonly Dictionary<uint, EquipmentCraftEntryPrimitive> equipmentCraft;

        public EquipmentBuilderBase(
            core.EquipmentType equipmentType,
            Predicate<EquipmentPrimitive> filter,
            EquipmentPackageProcessor equipments,
            LanguagePackageProcessor equipmentLanguages,
            EquipmentCraftPackageProcessor<core.EquipmentType> equipmentCraft,
            EquipmentUpgradePackageProcessor equipmentUpgrades
        )
        {
            this.equipmentType = equipmentType;
            this.filter = filter;
            this.equipments = equipments;
            this.equipmentLanguages = equipmentLanguages;
            this.equipmentUpgrades = equipmentUpgrades.Table[(byte)equipmentType];
            this.equipmentCraft = equipmentCraft.Table[equipmentType];
        }

        protected virtual bool IsValidEquipment(EquipmentPrimitive equipment)
        {
            if (equipment.Gender == core.Gender.None)
                return false;

            string englishWeaponName = equipmentLanguages.Table[LanguageIdPrimitive.English][equipment.GmdNameIndex].Value;
            if (LanguageUtils.IsValidText(englishWeaponName) == false)
                return false;

            string japaneseWeaponName = equipmentLanguages.Table[LanguageIdPrimitive.Japanese][equipment.GmdNameIndex].Value;
            if (LanguageUtils.IsValidText(japaneseWeaponName) == false)
                return false;

            string englishWeaponDescription = equipmentLanguages.Table[LanguageIdPrimitive.English][equipment.GmdNameIndex].Value;
            if (LanguageUtils.IsValidText(englishWeaponDescription) == false)
                return false;

            string japaneseWeaponDescription = equipmentLanguages.Table[LanguageIdPrimitive.Japanese][equipment.GmdDescriptionIndex].Value;
            if (LanguageUtils.IsValidText(japaneseWeaponDescription) == false)
                return false;

            return true;
        }

        protected virtual TEquiment CreateEquipmentInstance()
        {
            return new TEquiment();
        }

        protected virtual void UpdateEquipment(EquipmentPrimitive equipment, TEquiment resultEquipment)
        {
        }

        private static void TryAddCraft(List<core.CraftItem> crafts, ushort id, byte quantity)
        {
            if (quantity > 0)
                crafts.Add(new core.CraftItem { Id = id, Quantity = quantity });
        }

        private core.Craft CreateCraft(EquipmentPrimitive equipment)
        {
            bool isCraftable;
            var result = new List<core.CraftItem>();

            if (equipmentCraft.TryGetValue(equipment.Id, out EquipmentCraftEntryPrimitive craftEntry))
            {
                isCraftable = true;
                TryAddCraft(result, craftEntry.Item1Id, craftEntry.Item1Quantity);
                TryAddCraft(result, craftEntry.Item2Id, craftEntry.Item2Quantity);
                TryAddCraft(result, craftEntry.Item3Id, craftEntry.Item3Quantity);
                TryAddCraft(result, craftEntry.Item4Id, craftEntry.Item4Quantity);
            }
            else if (equipmentUpgrades.TryGetValue(equipment.Id, out EquipmentUpgradeEntryPrimitive upgradeEntry))
            {
                isCraftable = false;
                TryAddCraft(result, upgradeEntry.Item1Id, upgradeEntry.Item1Quantity);
                TryAddCraft(result, upgradeEntry.Item2Id, upgradeEntry.Item2Quantity);
                TryAddCraft(result, upgradeEntry.Item3Id, upgradeEntry.Item3Quantity);
                TryAddCraft(result, upgradeEntry.Item4Id, upgradeEntry.Item4Quantity);
            }
            else
                return null;

            return new core.Craft
            {
                IsCraftable = isCraftable,
                Items = result.OrderBy(x => x.Id).ToArray()
            };
        }


        private static readonly Func<string, string>[] languageValueProcessors = new Func<string, string>[]
        {   
            LanguageUtils.ReplaceLineFeedWithSpace
        };

        public TEquiment[] Build()
        {
            var result = new List<TEquiment>();

            foreach (EquipmentPrimitive equipment in equipments.Table.Values)
            {
                if (filter(equipment) == false)
                    continue;

                if (IsValidEquipment(equipment) == false)
                    continue;

                TEquiment resultEquipment = CreateEquipmentInstance();

                Dictionary<string, string> name = LanguageUtils.CreateLocalizations(equipmentLanguages.Table, equipment.GmdNameIndex);
                Dictionary<string, string> description = LanguageUtils.CreateLocalizations(equipmentLanguages.Table, equipment.GmdDescriptionIndex, languageValueProcessors);

                resultEquipment.Id = equipment.Id;
                resultEquipment.Order = equipment.Order;
                resultEquipment.Name = name;
                resultEquipment.Description = description;
                resultEquipment.Cost = equipment.CraftingCost;
                resultEquipment.Gender = equipment.Gender;
                resultEquipment.Rarity = equipment.Rarity;
                resultEquipment.SetGroup = equipment.SetGroup;

                core.Craft craft = CreateCraft(equipment);

                UpdateEquipment(equipment, resultEquipment);

                result.Add(resultEquipment);
            }

            return result.ToArray();
        }
    }
}
