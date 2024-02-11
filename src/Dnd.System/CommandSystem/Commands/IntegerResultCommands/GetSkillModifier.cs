namespace Dnd.System.CommandSystem.Commands.IntegerResultCommands;

using Dnd.System.CommandSystem.Commands.BooleanResultCommands;
using Dnd.System.Entities.Characters;
using Dnd.System.Entities.Skills;

public class GetSkillModifier : DndScoreCommand
{
    public GetSkillModifier(ICharacter character, ISkill skill) : base(character)
    {
        Skill = skill;
    }

    public ISkill Skill { get; }

    public override void InitializeResult()
    {
        var getAttributeModifierCommand = new GetAttributeModifier(Character, Skill.AttributeType);
        var attributeModifierResult = getAttributeModifierCommand.Execute();

        if (attributeModifierResult.IsSuccess)
        {
            Result.SetBaseValue(Character.AttributeSet.GetAttribute(Skill.AttributeType), attributeModifierResult.Value);

            // TODO: let expertise and proficiency bonus to be handled by feats

            var getHasSkillExpertise = new HasSkillExpertise(Character, Skill);
            var hasSkillExpertiseResult = getHasSkillExpertise.Execute();

            if (hasSkillExpertiseResult.IsSuccess && hasSkillExpertiseResult.Value)
            {
                var getProficiencyBonusCommand = new GetProficiencyBonus(Character);
                var proficiencyBonusResult = getProficiencyBonusCommand.Execute();

                if (proficiencyBonusResult.IsSuccess)
                {
                    Result.BonusCollection.AddBonus("Expertise Bonus", 2 * attributeModifierResult.Value);
                }
            }
            else
            {
                var getSkillProficiencyCommand = new HasSkillProficiency(Character, Skill);
                var skillProficiencyResult = getSkillProficiencyCommand.Execute();

                if (skillProficiencyResult.IsSuccess && skillProficiencyResult.Value)
                {
                    var getProficiencyBonusCommand = new GetProficiencyBonus(Character);
                    var proficiencyBonusResult = getProficiencyBonusCommand.Execute();

                    if (proficiencyBonusResult.IsSuccess)
                    {
                        Result.BonusCollection.AddBonus("Proficiency Bonus", proficiencyBonusResult.Value);
                    }
                }
            }
        }
        else
        {
            Result.SetError(attributeModifierResult.ErrorMessage ?? "Couldn't get attribute modifier");
        }
    }
}