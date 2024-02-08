namespace DnD.Commands;

using DnD.Entities.Characters;
using DnD.Entities.Skills;
using TableTopRpg.Commands;

internal class GetPassiveSkillValue : DndCommand
{
    public GetPassiveSkillValue(Character character, IDndSkill skill) : base(character)
    {
        Skill = skill;
    }

    public IDndSkill Skill { get; }

    protected override ICommandResult ExecuteDndCommand()
    {
        ICommand getSkillModifierCommand = new GetSkillModifierCommand(Character, Skill);
        ICommandResult skillModifierResult = getSkillModifierCommand.Execute();

        if (skillModifierResult.IsSuccess && skillModifierResult is IntegerResult integerResult)
        {
            var summarizedIntegerResult = SummarizedIntegerResult.Success(this, 10, "Base");
            summarizedIntegerResult.BonusValues.Add((integerResult.Value, Skill.Name + " Skill Modifier"));
            return summarizedIntegerResult;
        }
        else
        {
            return IntegerResult.Failure(this, skillModifierResult.Message);
        }
    }
}
