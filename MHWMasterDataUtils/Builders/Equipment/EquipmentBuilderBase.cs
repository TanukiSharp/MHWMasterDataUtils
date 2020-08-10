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
        protected readonly core.EquipmentType equipmentType;
        protected readonly Predicate<EquipmentPrimitive> filter;
        protected readonly EquipmentPackageProcessor equipments;
        protected readonly LanguagePackageProcessor equipmentLanguages;
        protected readonly Dictionary<uint, EquipmentCraftEntryPrimitive> equipmentCraft;

        public EquipmentBuilderBase(
            core.EquipmentType equipmentType,
            Predicate<EquipmentPrimitive> filter,
            EquipmentPackageProcessor equipments,
            LanguagePackageProcessor equipmentLanguages,
            EquipmentCraftPackageProcessor<core.EquipmentType> equipmentCraft
        )
        {
            this.equipmentType = equipmentType;
            this.filter = filter;
            this.equipments = equipments;
            this.equipmentLanguages = equipmentLanguages;
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

        protected static void TryAddCraft(List<core.CraftItem> crafts, ushort id, byte quantity)
        {
            if (quantity > 0)
                crafts.Add(new core.CraftItem { Id = id, Quantity = quantity });
        }

        protected virtual core.CraftItem[] CreateCraft(EquipmentPrimitive equipment)
        {
            if (equipmentCraft.TryGetValue(equipment.Id, out EquipmentCraftEntryPrimitive craftEntry) == false)
                return null;

            var result = new List<core.CraftItem>();

            TryAddCraft(result, craftEntry.Item1Id, craftEntry.Item1Quantity);
            TryAddCraft(result, craftEntry.Item2Id, craftEntry.Item2Quantity);
            TryAddCraft(result, craftEntry.Item3Id, craftEntry.Item3Quantity);
            TryAddCraft(result, craftEntry.Item4Id, craftEntry.Item4Quantity);

            return result.ToArray();
        }

        private static readonly LanguageUtils.LanguageValueProcessor[] languageValueProcessors = new[]
        {   
            LanguageUtils.ReplaceLineFeedWithSpace,
            LanguageUtils.ReplaceGreekLetterIcon,
            LanguageUtils.SpacingGreekLetter,
            LanguageUtils.StyleTextRemover
        };

        private static void TryAddRegularSkill(List<core.EquipmentSkill> setSkills, int id, int level)
        {
            if (id > 0)
                setSkills.Add(new core.EquipmentSkill { SkillId = id, Level = level, RequiredParts = null });
        }

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

                Dictionary<string, string> name = LanguageUtils.CreateLocalizations(equipmentLanguages.Table, equipment.GmdNameIndex, languageValueProcessors);
                Dictionary<string, string> description = LanguageUtils.CreateLocalizations(equipmentLanguages.Table, equipment.GmdDescriptionIndex, languageValueProcessors);

                resultEquipment.Id = equipment.Id;
                resultEquipment.Order = equipment.Order;
                resultEquipment.Name = name;
                resultEquipment.Description = description;
                resultEquipment.Cost = equipment.CraftingCost;
                resultEquipment.Gender = equipment.Gender;
                resultEquipment.Rarity = equipment.Rarity;
                resultEquipment.Craft = CreateCraft(equipment);
                resultEquipment.IsPermanent = equipment.IsPermanent == PermanentPrimitive.CannotBeSold;

                if (equipment.Skill1Id > 0 || equipment.Skill2Id > 0 || equipment.Skill3Id > 0)
                {
                    var skills = new List<core.EquipmentSkill>();

                    TryAddRegularSkill(skills, equipment.Skill1Id, equipment.Skill1Level);
                    TryAddRegularSkill(skills, equipment.Skill2Id, equipment.Skill2Level);
                    TryAddRegularSkill(skills, equipment.Skill3Id, equipment.Skill3Level);

                    resultEquipment.Skills = skills.ToArray();
                }

                UpdateEquipment(equipment, resultEquipment);

                result.Add(resultEquipment);
            }

            return result.ToArray();
        }
    }
}
