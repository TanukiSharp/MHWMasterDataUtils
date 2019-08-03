using System;
using System.Collections.Generic;
using System.Text;
using MHWMasterDataUtils.Equipment;
using MHWMasterDataUtils.Languages;

using core = MHWMasterDataUtils.Core;

namespace MHWMasterDataUtils.Builders.Equipment
{
    public abstract class ArmorPieceEquipmentBuilderBase<TArmorPiece> : EquipmentBuilderBase<TArmorPiece> where TArmorPiece : core.ArmorPiece, new()
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

        protected override void UpdateEquipment(EquipmentPrimitive equipment, TArmorPiece resultEquipment)
        {
            base.UpdateEquipment(equipment, resultEquipment);

            resultEquipment.SeriesId = equipment.SetId;
            resultEquipment.Defense = equipment.Defense;
            resultEquipment.FireResistance = equipment.FireRes;
            resultEquipment.WaterResistance = equipment.WaterRes;
            resultEquipment.ThunderResistance = equipment.ThunderRes;
            resultEquipment.IceResistance = equipment.IceRes;
            resultEquipment.DragonResistance = equipment.DragonRes;
            resultEquipment.Slots = EquipmentUtils.CreateSlotsArray(equipment);
            resultEquipment.SetGroup = equipment.SetGroup;
        }
    }
}
