using System;
using System.Collections.Generic;
using System.Text;
using MHWMasterDataUtils.Core;
using MHWMasterDataUtils.Equipment;
using MHWMasterDataUtils.Languages;

namespace MHWMasterDataUtils.Builders.Equipment
{
    public class CharmEquipmentBuilder : EquipmentBuilderBase<Charm>
    {
        public CharmEquipmentBuilder(
            EquipmentPackageProcessor equipments,
            LanguagePackageProcessor equipmentLanguages
        )
            : base(
                  x => x.EquipSlot == EquipmentTypePrimitive.Charm,
                  equipments,
                  equipmentLanguages
            )
        {
        }
    }
}
