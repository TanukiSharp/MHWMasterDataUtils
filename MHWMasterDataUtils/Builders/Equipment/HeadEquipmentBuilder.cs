using System;
using System.Collections.Generic;
using System.Text;
using MHWMasterDataUtils.Core;
using MHWMasterDataUtils.Equipment;
using MHWMasterDataUtils.Languages;

namespace MHWMasterDataUtils.Builders.Equipment
{
    public class HeadEquipmentBuilder : ArmorPieceEquipmentBuilderBase<ArmorPiece>
    {
        public HeadEquipmentBuilder(
            EquipmentPackageProcessor equipments,
            LanguagePackageProcessor equipmentLanguages,
            EquipmentCraftPackageProcessor<EquipmentType> equipmentCraft
        )
            : base(
                  EquipmentType.Head,
                  x => x.EquipSlot == EquipmentTypePrimitive.Head && x.Type == ArmorTypePrimitive.Regular || x.Type == ArmorTypePrimitive.FullSet,
                  equipments,
                  equipmentLanguages,
                  equipmentCraft
            )
        {
        }
    }
}
