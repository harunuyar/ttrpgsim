namespace Dnd.Predefined.Traits.Dragonborn;

using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.CommandSystem.Commands.IntegerResultCommands;
using Dnd.System.Entities.Attributes;

public class AbilityScoreIncrease : ATrait
{
    public static readonly AbilityScoreIncrease Instance = new AbilityScoreIncrease();

    private AbilityScoreIncrease() : base("Ability Score Increase", "Your Strength score increases by 2, and your Charisma score increases by 1.")
    {
    }

    public override void HandleCommand(ICommand command)
    {
        if (command is GetAttributeScore getAttributeScoreCommand)
        {
            if (getAttributeScoreCommand.AttributeType == EAttributeType.Strength)
            {
                getAttributeScoreCommand.AddBonus(this, 2);
            }
            else if (getAttributeScoreCommand.AttributeType == EAttributeType.Charisma)
            {
                getAttributeScoreCommand.AddBonus(this, 1);
            }
        }
    }
}
