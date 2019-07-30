using System;
using System.Collections.Generic;
using System.Text;
using MHWMasterDataUtils.Armors;
using MHWMasterDataUtils.Core;
using MHWMasterDataUtils.Equipments;
using MHWMasterDataUtils.Languages;

namespace MHWMasterDataUtils.Builders.Equipment
{
    public class CharmEquipmentBuilder : EquipmentBuilderBase<Charm>
    {
        public CharmEquipmentBuilder(
            ArmorPackageProcessor equipments,
            LanguagePackageProcessor equipmentLanguages,
            LanguagePackageProcessor equipmentSeriesLanguages

        )
            : base(
                  x => x.EquipSlot == EquipmentTypePrimitive.Charm,
                  equipments,
                  equipmentLanguages,
                  equipmentSeriesLanguages
            )
        {
        }
    }
}
