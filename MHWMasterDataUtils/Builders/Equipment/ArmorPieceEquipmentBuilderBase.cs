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
        public ArmorPieceEquipmentBuilderBase(
            Predicate<ArmorPrimitive> filter,
            ArmorPackageProcessor equipments,
            LanguagePackageProcessor equipmentLanguages,
            LanguagePackageProcessor equipmentSeriesLanguages

        )
            : base(
                  filter,
                  equipments,
                  equipmentLanguages,
                  equipmentSeriesLanguages
            )
        {
        }

        protected override void UpdateEquipment(ArmorPrimitive equipment, TArmorPiece resultEquipment)
        {
            base.UpdateEquipment(equipment, resultEquipment);

            resultEquipment.SeriesId = equipment.SetId;
        }
    }
}
