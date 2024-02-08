namespace DnD.Commands;

using DnD.Entities.Characters;
using DnD.Entities.Skills;
using TableTopRpg.Commands;

internal class GetSkillModifierCommand : DndCommand
{
    public GetSkillModifierCommand(Character character, IDndSkill skill) : base(character)
    {
        Skill = skill;
    }

    public IDndSkill Skill { get; }

    protected override ICommandResult ExecuteDndCommand()
    {
        try
        {
            ICommand getSkillProficiencyCommand = new GetSkillProficiencyCommand(Character, Skill);
            ICommandResult skillProficiencyResult = getSkillProficiencyCommand.Execute();

            if (skillProficiencyResult.IsSuccess && skillProficiencyResult is IntegerResult skillProficiencyIntegerResult)
            {
                ICommand getProficiencyBonusCommand = new GetProficiencyBonusCommand(Character);
                ICommandResult proficiencyBonusResult = getProficiencyBonusCommand.Execute();

                if (proficiencyBonusResult.IsSuccess && proficiencyBonusResult is IntegerResult proficiencyBonusIntegerResult)
                {
                    var attribute = Character.AttributeSet.GetAttribute(Skill.AttributeType);
                    var summarizedIntegerResult = SummarizedIntegerResult.Success(this, attribute.GetModifier(), attribute.Name + " Modifier");

                    if (skillProficiencyIntegerResult.Value > 0)
                    {
                        summarizedIntegerResult.BonusValues.Add((skillProficiencyIntegerResult.Value * proficiencyBonusIntegerResult.Value, "Proficiency Bonus"));
                    }

                    return summarizedIntegerResult;
                }
                else
                {
                    return IntegerResult.Failure(this, proficiencyBonusResult.Message);
                }
            }
            else
            {
                return IntegerResult.Failure(this, skillProficiencyResult.Message);
            }
        }
        catch (Exception e)
        {
            return IntegerResult.Failure(this, e.Message);
        }
    }
}