namespace Dnd.Predefined.Traits.HalfElf;

using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.CommandSystem.Commands.IntegerResultCommands;
using Dnd.System.Entities.Attributes;

public class AbilityScoreIncrease : ATrait
{
    public AbilityScoreIncrease(EAttributeType attribute1, EAttributeType attribute2) : base("Ability Score Increase", "Your Charisma score increases by 2, and two other ability scores of your choice increase by 1.")
    {
        Attribute1 = attribute1;
        Attribute2 = attribute2;
    }

    public EAttributeType Attribute1 { get; }

    public EAttributeType Attribute2 { get; }

    public override void HandleCommand(ICommand command)
    {
        if (command is GetBaseAttributeScore getAttributeScore)
        {
            if (getAttributeScore.AttributeType == EAttributeType.Charisma)
            {
                getAttributeScore.AddBonus(this, 2);
            }
            else if (getAttributeScore.AttributeType == Attribute1)
            {
                getAttributeScore.AddBonus(this, 1);
            }
            else if (getAttributeScore.AttributeType == Attribute2)
            {
                getAttributeScore.AddBonus(this, 1);
            }
        }
    }
}
