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
            Predicate<EquipmentPrimitive> filter,
            EquipmentPackageProcessor equipments,
            LanguagePackageProcessor equipmentLanguages

        )
            : base(
                  filter,
                  equipments,
                  equipmentLanguages
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
        }
    }
}
