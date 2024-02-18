namespace Dnd.Predefined.Traits.Dragonborn;

using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.CommandSystem.Commands.IntegerResultCommands;
using Dnd.System.Entities.Attributes;

public class AbilityScoreIncrease : ATrait
{
    public AbilityScoreIncrease() : base("Ability Score Increase", "Your Strength score increases by 2, and your Charisma score increases by 1.")
    {
    }

    public override void HandleCommand(ICommand command)
    {
        if (command is GetBaseAttributeScore getBaseAttributeScore)
        {
            if (getBaseAttributeScore.AttributeType == EAttributeType.Strength)
            {
                getBaseAttributeScore.AddBonus(this, 2);
            }
            else if (getBaseAttributeScore.AttributeType == EAttributeType.Charisma)
            {
                getBaseAttributeScore.AddBonus(this, 1);
            }
        }
    }
}
