namespace DnD.CommandSystem.Commands;

using DnD.CommandSystem.CommandResult;
using DnD.Entities.Characters;
using DnD.Entities.Skills;

internal class GetSkillProficiencyCommand : ICommand
{
    public GetSkillProficiencyCommand(Player player, Skill skill)
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
            return IntegerResult.Success(this, Player.GetSkillProficiency(Skill));
        }
        catch (Exception e)
        {
            return IntegerResult.Failure(this, e.Message);
        }
    }
}