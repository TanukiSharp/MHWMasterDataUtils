using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MHWMasterDataUtils.Languages;
using MHWMasterDataUtils.Skills;

using core = MHWMasterDataUtils.Core;

namespace MHWMasterDataUtils.Builders
{
    public class SkillBuilder
    {
        private readonly SkillsPackageProcessor skills;
        private readonly SkillAbilitiesPackageProcessor skillAbilities;
        private readonly LanguagePackageProcessor skillsLanguages;
        private readonly LanguagePackageProcessor skillAttributesLanguages;

        public SkillBuilder(
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

        private static readonly LanguageUtils.LanguageValueProcessor[] languageValueProcessors = new[]
        {
            LanguageUtils.ReplaceLineFeedWithSpace
        };

        public core.Skill[] Build()
        {
            var result = new List<core.Skill>();

            foreach (KeyValuePair<ushort, Dictionary<byte, SkillAbilityPrimitive>> skillGroup in skillAbilities.Table)
            {
                if (skillGroup.Key == 0)
                    continue;

                SkillPrimitive skill = skills.List[skillGroup.Key];

                uint skillNameIndex = (uint)(skillGroup.Key * 3);
                uint skillReadingIndex = skillNameIndex + 1;
                uint skillDescriptionIndex = skillNameIndex + 2;

                if (LanguageUtils.IsValidText(skillsLanguages.Table, skillNameIndex) == false)
                    continue;

                bool isSetBonus = skill.IsSetBonus != 0;

                Dictionary<string, string> skillName = LanguageUtils.CreateLocalizations(skillsLanguages.Table, skillNameIndex);
                Dictionary<string, string> skillReading = LanguageUtils.CreateLocalizations(skillsLanguages.Table, skillReadingIndex);
                Dictionary<string, string> skillDescription = LanguageUtils.CreateLocalizations(skillsLanguages.Table, skillDescriptionIndex, languageValueProcessors);

                var abilities = new List<core.Ability>();

                foreach (SkillAbilityPrimitive skillAbility in skillGroup.Value.Values)
                {
                    if (skillAbility.SkillId == 0)
                        continue;

                    uint skillAbilityNameIndex = skillAbility.Index * 2;
                    uint skillAbilityDescriptionIndex = skillAbilityNameIndex + 1;

                    Dictionary<string, string> skillAbilityName = LanguageUtils.CreateLocalizations(skillAttributesLanguages.Table, skillAbilityNameIndex);
                    Dictionary<string, string> skillAbilityDescription = LanguageUtils.CreateLocalizations(skillAttributesLanguages.Table, skillAbilityDescriptionIndex, languageValueProcessors);

                    int[] parameters = new int[]
                    {
                        skillAbility.Param1,
                        skillAbility.Param2,
                        skillAbility.Param3,
                        skillAbility.Param4
                    };

                    if (isSetBonus)
                    {
                        abilities.Add(core.Ability.CreateSetSkill(
                            skillAbility.Level,
                            skillAbilityName,
                            skillAbilityDescription,
                            parameters
                        ));
                    }
                    else
                    {
                        abilities.Add(core.Ability.CreateRegularSkill(
                            skillAbility.Level,
                            skillAbilityDescription,
                            parameters
                        ));
                    }
                }

                result.Add(new core.Skill
                {
                    Id = skillGroup.Key,
                    Category = skill.Unknown,
                    IsSetBonus = isSetBonus,
                    Name = skillName,
                    Reading = LanguageUtils.IsValidText(skillReading) ? skillReading : null,
                    Description = isSetBonus ? null : skillDescription,
                    Abilities = abilities.ToArray()
                });
            }

            return result.ToArray();
        }
    }
}
