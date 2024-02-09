namespace DnD.CommandSystem.Commands.IntegerResultCommands;

using DnD.CommandSystem.Results;
using DnD.Entities.Characters;
using DnD.Entities.Skills;

internal class GetSkillModifier : DndScoreCommand
{
    public GetSkillModifier(Character character, IDndSkill skill) : base(character)
    {
        Skill = skill;
    }

    public IDndSkill Skill { get; }

    public override void CollectBonuses()
    {
        var getAttributeModifierCommand = new GetAttributeModifier(Character, Skill.AttributeType);
        getAttributeModifierCommand.CollectBonuses();
        var attributeModifierResult = getAttributeModifierCommand.Execute();

        if (attributeModifierResult.IsSuccess)
        {
            IntegerBonuses.AddBonus(Skill.AttributeType.ToString(), attributeModifierResult.Value);
        }

        var getSkillProficiencyCommand = new GetSkillProficiency(Character, Skill);
        var skillProficiencyResult = getSkillProficiencyCommand.Execute();

        if (skillProficiencyResult.IsSuccess && skillProficiencyResult.Value > 0)
        {
            var getProficiencyBonusCommand = new GetProficiencyBonus(Character);
            var proficiencyBonusResult = getProficiencyBonusCommand.Execute();

            if (proficiencyBonusResult.IsSuccess && proficiencyBonusResult.Value > 0)
            {
                IntegerBonuses.AddBonus("Proficiency Bonus", skillProficiencyResult.Value * proficiencyBonusResult.Value);
            }
        }

        base.CollectBonuses();
    }

    public override IntegerBonuses Execute()
    {
        return IntegerBonuses;
    }
}