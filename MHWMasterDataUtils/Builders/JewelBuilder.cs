using System;
using System.Collections.Generic;
using System.Text;
using MHWMasterDataUtils.Core;
using MHWMasterDataUtils.Items;
using MHWMasterDataUtils.Jewels;
using MHWMasterDataUtils.Languages;

namespace MHWMasterDataUtils.Builders
{
    public class JewelBuilder : ItemBuilder<Jewel>
    {
        private readonly JewelPackageProcessor jewels;

        public JewelBuilder(
            ItemsPackageProcessor items,
            LanguagePackageProcessor steamItemsLanguages,
            JewelPackageProcessor jewels
        )
            : base(
                  i => i.Type == ItemTypePrimitive.Jewel,
                  items,
                  steamItemsLanguages
            )
        {
            this.jewels = jewels;
        }

        protected override void UpdateItem(Jewel item)
        {
            base.UpdateItem(item);

            item.EquipmentId = jewels.Table[item.Id].EquipmentId;
        }
    }
}
