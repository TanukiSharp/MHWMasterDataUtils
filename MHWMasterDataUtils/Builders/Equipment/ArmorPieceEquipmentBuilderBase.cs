using System;
using System.Collections.Generic;
using System.Text;
using MHWMasterDataUtils.Armors;
using MHWMasterDataUtils.Core;
using MHWMasterDataUtils.Equipments;
using MHWMasterDataUtils.Languages;

namespace MHWMasterDataUtils.Builders.Equipment
{
    public abstract class ArmorPieceEquipmentBuilderBase<TArmorPiece> : EquipmentBuilderBase<TArmorPiece> where TArmorPiece : ArmorPiece, new()
    {
        private readonly LanguagePackageProcessor equipmentSeriesLanguages;

        public ArmorPieceEquipmentBuilderBase(
            Predicate<ArmorPrimitive> filter,
            ArmorPackageProcessor equipments,
            LanguagePackageProcessor equipmentLanguages,
            LanguagePackageProcessor equipmentSeriesLanguages

        )
            : base(
                  filter,
                  equipments,
                  equipmentLanguages
            )
        {
            this.equipmentSeriesLanguages = equipmentSeriesLanguages;
        }

        protected override void UpdateEquipment(ArmorPrimitive equipment, TArmorPiece resultEquipment)
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
