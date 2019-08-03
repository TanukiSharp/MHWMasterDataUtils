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

            JewelPrimitive jewel = jewels.Table[item.Id];

            item.EquipmentId = jewel.EquipmentId;

            var skills = new List<JewelSkill>();

            if (jewel.Skill1Id > 0)
                skills.Add(new JewelSkill { SkillId = jewel.Skill1Id, Level = jewel.Skill1Level });

            if (jewel.Skill2Id > 0)
                skills.Add(new JewelSkill { SkillId = jewel.Skill2Id, Level = jewel.Skill2Level });

            if (skills.Count > 0)
                item.Skills = skills.ToArray();
        }
    }
}
