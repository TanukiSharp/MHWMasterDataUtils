using System;
using System.Collections.Generic;
using System.Text;
using MHWMasterDataUtils.Armors;
using MHWMasterDataUtils.Core;
using MHWMasterDataUtils.Equipments;
using MHWMasterDataUtils.Languages;

namespace MHWMasterDataUtils.Builders.Equipment
{
    public class HeadEquipmentBuilder : ArmorPieceEquipmentBuilderBase<ArmorPiece>
    {
        public HeadEquipmentBuilder(
            ArmorPackageProcessor equipments,
            LanguagePackageProcessor equipmentLanguages,
            LanguagePackageProcessor equipmentSeriesLanguages

        )
            : base(
                  x => x.EquipSlot == EquipmentTypePrimitive.Head && x.Type == ArmorTypePrimitive.Regular || x.Type == ArmorTypePrimitive.FullSet,
                  equipments,
                  equipmentLanguages,
                  equipmentSeriesLanguages
            )
        {
        }
    }
}
