using System;
using System.Collections.Generic;
using System.Text;
using MHWMasterDataUtils.Equipment;
using MHWMasterDataUtils.Languages;

using core = MHWMasterDataUtils.Core;

namespace MHWMasterDataUtils.Builders.Equipment
{
    public class ArmorPieceEquipmentBuilder : EquipmentBuilderBase<core.ArmorPiece>
    {
        public ArmorPieceEquipmentBuilder(
            core.EquipmentType equipmentType,
            EquipmentPackageProcessor equipments,
            LanguagePackageProcessor equipmentLanguages,
            EquipmentCraftPackageProcessor<core.EquipmentType> equipmentCraft

        )
            : base(
                  equipmentType,
                  x => x.EquipSlot == equipmentType && x.Type == ArmorTypePrimitive.Regular || x.Type == ArmorTypePrimitive.FullSet,
                  equipments,
                  equipmentLanguages,
                  equipmentCraft
            )
        {
        }

        protected override void UpdateEquipment(EquipmentPrimitive equipment, core.ArmorPiece resultEquipment)
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

            if (equipment.SetSkill1Id > 0 || equipment.SetSkill2Id > 0)
            {
                var setSkills = new List<int>();

                if (equipment.SetSkill1Id > 0)
                    setSkills.Add(equipment.SetSkill1Id);
                if (equipment.SetSkill2Id > 0)
                    setSkills.Add(equipment.SetSkill2Id);

                resultEquipment.SetSkills = setSkills.ToArray();
            }
        }
    }
}
