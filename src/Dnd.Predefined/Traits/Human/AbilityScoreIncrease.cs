namespace Dnd.Predefined.Traits.Human;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.CommandSystem.Commands.IntegerResultCommands;

public class AbilityScoreIncrease : ATrait
{
    public static readonly AbilityScoreIncrease Instance = new AbilityScoreIncrease();

    private AbilityScoreIncrease() : base("Ability Score Increase", "Your ability scores each increase by 1.")
    {
    }

    public override void HandleCommand(DndCommand command)
    {
        base.HandleCommand(command);

        if (command is GetAttributeScore getAttributeScoreCommand)
        {
            getAttributeScoreCommand.Result.BonusCollection.AddBonus(this, 1);
        }
    }
}
