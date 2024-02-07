namespace DnD.CommandSystem.Commands;

using DnD.CommandSystem.CommandResult;
using DnD.Entities.Characters;
using DnD.Entities.Skills;

internal class GetSkillModifierCommand : ICommand
{
    public GetSkillModifierCommand(Player player, Skill skill)
    {
        Player = player;
        Skill = skill;
    }

    public Player Player { get; }
    public Skill Skill { get; }

    public ICommandResult Execute()
    {
        try
        {
            ICommand getSkillProficiencyCommand = new GetSkillProficiencyCommand(Player, Skill);
            ICommandResult skillProficiencyResult = getSkillProficiencyCommand.Execute();

            if (skillProficiencyResult.IsSuccess && skillProficiencyResult is IntegerResult integerResult)
            {
                return IntegerResult.Success(this, Skill.GetModifier(Player.AttributeSet, integerResult.Value));
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