namespace Dnd.Entities.Traits.Dwarf;

using Dnd.CommandSystem.Commands;
using Dnd.CommandSystem.Commands.IntegerResultCommands;

public class AbilityScoreIncrease : ATrait
{
    public static readonly AbilityScoreIncrease Instance = new AbilityScoreIncrease();

    private AbilityScoreIncrease() : base("Ability Score Increase", "Your Constitution score increases by 2.")
    {
    }

    public override void HandleCommand(DndCommand command)
    {
        if (command is GetAttributeScore getAttributeScore)
        {
            if (getAttributeScore.AttributeType == Attributes.EAttributeType.Constitution)
            {
                getAttributeScore.Result.BonusCollection.AddBonus(this, 2);
            }
        }
    }
}
