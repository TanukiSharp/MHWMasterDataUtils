using System;
using System.Collections.Generic;
using System.Text;
using MHWMasterDataUtils.Equipment;
using MHWMasterDataUtils.Languages;

using core = MHWMasterDataUtils.Core;

namespace MHWMasterDataUtils.Builders.Equipment
{
    public abstract class ArmorPieceEquipmentBuilderBase<TArmorPiece> : EquipmentBuilderBase<TArmorPiece>
        where TArmorPiece : core.ArmorPiece, new()
    {
        public ArmorPieceEquipmentBuilderBase(
            core.EquipmentType equipmentType,
            Predicate<EquipmentPrimitive> filter,
            EquipmentPackageProcessor equipments,
            LanguagePackageProcessor equipmentLanguages,
            EquipmentCraftPackageProcessor<core.EquipmentType> equipmentCraft

        )
            : base(
                  equipmentType,
                  filter,
                  equipments,
                  equipmentLanguages,
                  equipmentCraft
            )
        {
        }

        private static void TryAddRegularSkill(List<core.EquipmentSkill> setSkills, int id, int level)
        {
            if (id > 0)
                setSkills.Add(new core.EquipmentSkill { SkillId = id, Level = level, RequiredParts = null });
        }

        protected override void UpdateEquipment(EquipmentPrimitive equipment, TArmorPiece resultEquipment)
        {
            base.UpdateEquipment(equipment, resultEquipment);

            resultEquipment.SeriesId = equipment.SetId;
            resultEquipment.Defense = equipment.Defense;
            resultEquipment.ElementalResistances = new core.ElementalResistances
            {
                Fire = equipment.FireRes,
                Water = equipment.WaterRes,
                Thunder = equipment.ThunderRes,
                Ice = equipment.IceRes,
                Dragon = equipment.DragonRes
            };
            resultEquipment.Slots = EquipmentUtils.CreateSlotsArray(equipment);
            resultEquipment.SetGroup = equipment.SetGroup;

            if (equipment.SetSkill1Id > 0 || equipment.SetSkill2Id > 0)
            {
                var setSkills = new List<int>();

                if (equipment.SetSkill1Id > 0)
                    setSkills.Add(equipment.SetSkill1Id);
                if (equipment.SetSkill2Id > 0)
                    setSkills.Add(equipment.SetSkill2Id);

                resultEquipment.SetSkills = setSkills.ToArray();
            }

            if (equipment.Skill1Id > 0 || equipment.Skill2Id > 0 || equipment.Skill3Id > 0)
            {
                var skills = new List<core.EquipmentSkill>();

                TryAddRegularSkill(skills, equipment.Skill1Id, equipment.Skill1Level);
                TryAddRegularSkill(skills, equipment.Skill2Id, equipment.Skill2Level);
                TryAddRegularSkill(skills, equipment.Skill3Id, equipment.Skill3Level);

                resultEquipment.Skills = skills.ToArray();
            }
        }
    }
}
