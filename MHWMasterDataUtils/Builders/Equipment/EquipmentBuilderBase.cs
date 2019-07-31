using System;
using System.Collections.Generic;
using System.Text;
using MHWMasterDataUtils.Equipment;
using MHWMasterDataUtils.Languages;

using core = MHWMasterDataUtils.Core;

namespace MHWMasterDataUtils.Builders.Equipment
{
    public abstract class EquipmentBuilderBase<TEquiment> where TEquiment : core.EquipmentBase, new()
    {
        private readonly Predicate<EquipmentPrimitive> filter;
        private readonly EquipmentPackageProcessor equipments;
        private readonly LanguagePackageProcessor equipmentLanguages;

        public EquipmentBuilderBase(
            Predicate<EquipmentPrimitive> filter,
            EquipmentPackageProcessor equipments,
            LanguagePackageProcessor equipmentLanguages
        )
        {
            this.filter = filter;
            this.equipments = equipments;
            this.equipmentLanguages = equipmentLanguages;
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

                UpdateEquipment(equipment, resultEquipment);

                result.Add(resultEquipment);
            }

            return result.ToArray();
        }
    }
}
