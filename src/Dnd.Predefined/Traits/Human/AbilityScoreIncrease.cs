namespace Dnd.Predefined.Traits.Human;

using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.CommandSystem.Commands.IntegerResultCommands;

public class AbilityScoreIncrease : ATrait
{
    public AbilityScoreIncrease() : base("Ability Score Increase", "Your ability scores each increase by 1.")
    {
    }

    public override void HandleCommand(ICommand command)
    {
        base.HandleCommand(command);

        if (command is GetBaseAttributeScore getAttributeScore)
        {
            getAttributeScore.AddBonus(this, 1);
        }
    }
}
