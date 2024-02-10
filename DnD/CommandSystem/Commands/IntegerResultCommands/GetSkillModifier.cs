namespace DnD.CommandSystem.Commands.IntegerResultCommands;

using DnD.Entities.Characters;
using DnD.Entities.Skills;

internal class GetSkillModifier : DndScoreCommand
{
    public GetSkillModifier(Character character, IDndSkill skill) : base(character)
    {
        Skill = skill;
    }

    public IDndSkill Skill { get; }

    public override void InitializeResult()
    {
        var getAttributeModifierCommand = new GetAttributeModifier(Character, Skill.AttributeType);
        var attributeModifierResult = getAttributeModifierCommand.Execute();

        if (attributeModifierResult.IsSuccess)
        {
            Result.SetBaseValue(Character.AttributeSet.GetAttribute(Skill.AttributeType), attributeModifierResult.Value);

            var getSkillProficiencyCommand = new GetSkillProficiency(Character, Skill);
            var skillProficiencyResult = getSkillProficiencyCommand.Execute();

            if (skillProficiencyResult.IsSuccess && skillProficiencyResult.Value > 0)
            {
                var getProficiencyBonusCommand = new GetProficiencyBonus(Character);
                var proficiencyBonusResult = getProficiencyBonusCommand.Execute();

                if (proficiencyBonusResult.IsSuccess)
                {
                    Result.BonusCollection.AddBonus("Proficiency Bonus", skillProficiencyResult.Value * proficiencyBonusResult.Value);
                }
            }
        }
        else
        {
            Result.SetError(attributeModifierResult.ErrorMessage ?? "Couldn't get attribute modifier");
        }
    }
}