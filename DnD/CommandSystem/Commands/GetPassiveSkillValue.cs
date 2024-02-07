namespace DnD.CommandSystem.Commands;

using DnD.CommandSystem.CommandResult;
using DnD.Entities.Characters;
using DnD.Entities.Skills;

internal class GetPassiveSkillValue : ICommand
{
    public GetPassiveSkillValue(Player player, Skill skill)
    {
        Player = player;
        Skill = skill;
    }

    public Player Player { get; }
    public Skill Skill { get; }

    public ICommandResult Execute()
    {
        ICommand getSkillModifierCommand = new GetSkillModifierCommand(Player, Skill);
        ICommandResult skillModifierResult = getSkillModifierCommand.Execute();

        if (skillModifierResult.IsSuccess && skillModifierResult is IntegerResult integerResult)
        {
            return IntegerResult.Success(this, integerResult.Value + 10);
        }
        else
        {
            return IntegerResult.Failure(this, skillModifierResult.Message);
        }
    }
}
