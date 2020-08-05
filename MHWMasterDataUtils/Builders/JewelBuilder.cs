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

        protected override bool UpdateItem(Jewel item)
        {
            if (base.UpdateItem(item) == false)
                return false;

            if (jewels.Table.TryGetValue(item.Id, out JewelPrimitive jewel) == false)
                return false;

            item.EquipmentId = jewel.EquipmentId;

            if (jewel.Skill1Id > 0 || jewel.Skill2Id > 0)
            {
                var skills = new List<EquipmentSkill>();

                if (jewel.Skill1Id > 0)
                    skills.Add(new EquipmentSkill { SkillId = (int)jewel.Skill1Id, Level = (int)jewel.Skill1Level });

                if (jewel.Skill2Id > 0)
                    skills.Add(new EquipmentSkill { SkillId = (int)jewel.Skill2Id, Level = (int)jewel.Skill2Level });

                item.Skills = skills.ToArray();
            }

            return true;
        }
    }
}
