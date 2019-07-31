﻿using System;
using System.Collections.Generic;
using System.Text;
using MHWMasterDataUtils.Armors;
using MHWMasterDataUtils.Equipments;
using MHWMasterDataUtils.Languages;

using core = MHWMasterDataUtils.Core;

namespace MHWMasterDataUtils.Builders.Equipment
{
    public abstract class EquipmentBuilderBase<TEquiment> where TEquiment : core.EquipmentBase, new()
    {
        private readonly Predicate<ArmorPrimitive> filter;
        private readonly ArmorPackageProcessor equipments;
        private readonly LanguagePackageProcessor equipmentLanguages;

        public EquipmentBuilderBase(
            Predicate<ArmorPrimitive> filter,
            ArmorPackageProcessor equipments,
            LanguagePackageProcessor equipmentLanguages
        )
        {
            this.filter = filter;
            this.equipments = equipments;
            this.equipmentLanguages = equipmentLanguages;
        }

        protected virtual bool IsValidEquipment(ArmorPrimitive equipment)
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

        protected virtual void UpdateEquipment(ArmorPrimitive equipment, TEquiment resultEquipment)
        {
        }

        public TEquiment[] Build()
        {
            var result = new List<TEquiment>();

            var languageValueProcessors = new Func<string, string>[]
            {
                LanguageUtils.ReplaceLineFeedWithSpace
            };

            foreach (ArmorPrimitive equipment in equipments.Table.Values)
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