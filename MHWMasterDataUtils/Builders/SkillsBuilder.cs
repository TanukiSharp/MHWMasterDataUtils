using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MHWMasterDataUtils.Languages;
using MHWMasterDataUtils.Skills;

using core = MHWMasterDataUtils.Core;

namespace MHWMasterDataUtils.Builders
{
    public class SkillsBuilder
    {
        private readonly SkillsPackageProcessor skills;
        private readonly SkillAbilitiesPackageProcessor skillAbilities;
        private readonly LanguagePackageProcessor skillsLanguages;
        private readonly LanguagePackageProcessor skillAttributesLanguages;

        public SkillsBuilder(
            SkillsPackageProcessor skills,
            SkillAbilitiesPackageProcessor skillAbilities,
            LanguagePackageProcessor skillsLanguages,
            LanguagePackageProcessor skillAttributesLanguages
        )
        {
            this.skills = skills;
            this.skillAbilities = skillAbilities;
            this.skillsLanguages = skillsLanguages;
            this.skillAttributesLanguages = skillAttributesLanguages;
        }

        public core.Skill[] Build()
        {
            uint skillAttributeIndex = 0;

            var result = new List<core.Skill>();

            foreach (KeyValuePair<ushort, Dictionary<byte, SkillAbilityPrimitive>> skillGroup in skillAbilities.Table)
            {
                if (skillGroup.Key == 0)
                {
                    skillAttributeIndex++;
                    continue;
                }

                SkillPrimitive skill = skills.List[skillGroup.Key];

                uint skillNameIndex = (uint)(skillGroup.Key * 3);
                uint skillReadingIndex = skillNameIndex + 1;
                uint skillDescriptionIndex = skillNameIndex + 2;

                if (LanguageUtils.IsValidText(skillsLanguages.Table, skillNameIndex) == false)
                {
                    skillAttributeIndex += (uint)skillGroup.Value.Count;
                    continue;
                }

                bool isSetBonus = skill.IsSetBonus != 0;

                if (isSetBonus)
                    throw new NotImplementedException();

                Dictionary<string, string> skillName = LanguageUtils.CreateLocalizations(skillsLanguages.Table, skillNameIndex);
                Dictionary<string, string> skillReading = LanguageUtils.CreateLocalizations(skillsLanguages.Table, skillReadingIndex);
                Dictionary<string, string> skillDescription = LanguageUtils.CreateLocalizations(skillsLanguages.Table, skillDescriptionIndex);

                var abilities = new List<core.Ability>();

                foreach (KeyValuePair<byte, SkillAbilityPrimitive> skillAbility in skillGroup.Value)
                {
                    uint skillAbilityNameIndex = (uint)(skillAttributeIndex * 2);
                    uint skillAbilityDescriptionIndex = skillAbilityNameIndex + 1;

                    Dictionary<string, string> skillAbilityName = LanguageUtils.CreateLocalizations(skillAttributesLanguages.Table, skillAbilityNameIndex);
                    Dictionary<string, string> skillAbilityDescription = LanguageUtils.CreateLocalizations(skillAttributesLanguages.Table, skillAbilityDescriptionIndex);

                    abilities.Add(new core.Ability
                    {
                        Name = isSetBonus ? skillAbilityName : null,
                        Description = skillAbilityDescription,
                        Level = skillAbility.Value.Level,
                        Parameters = new int[]
                        {
                            skillAbility.Value.Param1,
                            skillAbility.Value.Param2,
                            skillAbility.Value.Param3,
                            skillAbility.Value.Param4
                        }
                    });

                    skillAttributeIndex++;
                }

                result.Add(new core.Skill
                {
                    Id = skillGroup.Key,
                    Category = skill.Unknown,
                    IsSetBonus = isSetBonus,
                    Name = skillName,
                    Reading = LanguageUtils.IsValidText(skillReading) ? skillReading : null,
                    Description = skillDescription,
                    Abilities = abilities.ToArray()
                });
            }

            return result.ToArray();
        }
    }
}
