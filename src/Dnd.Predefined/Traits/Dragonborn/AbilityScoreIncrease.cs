namespace Dnd.Predefined.Traits.Dragonborn;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.CommandSystem.Commands.IntegerResultCommands;
using Dnd.System.Entities.Attributes;

public class AbilityScoreIncrease : ATrait
{
    public static readonly AbilityScoreIncrease Instance = new AbilityScoreIncrease();

    private AbilityScoreIncrease() : base("Ability Score Increase", "Your Strength score increases by 2, and your Charisma score increases by 1.")
    {
    }

    public override void HandleCommand(DndCommand command)
    {
        if (command is GetAttributeScore getAttributeScoreCommand)
        {
            if (getAttributeScoreCommand.AttributeType == EAttributeType.Strength)
            {
                getAttributeScoreCommand.Result.BonusCollection.AddBonus(this, 2);
            }
            else if (getAttributeScoreCommand.AttributeType == EAttributeType.Charisma)
            {
                getAttributeScoreCommand.Result.BonusCollection.AddBonus(this, 1);
            }
        }
    }
}
