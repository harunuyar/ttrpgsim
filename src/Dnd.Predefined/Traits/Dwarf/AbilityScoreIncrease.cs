﻿namespace Dnd.Predefined.Traits.Dwarf;

using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.CommandSystem.Commands.IntegerResultCommands;
using Dnd.System.Entities.Attributes;

public class AbilityScoreIncrease : ATrait
{
    public AbilityScoreIncrease() : base("Ability Score Increase", "Your Constitution score increases by 2.")
    {
    }

    public override void HandleCommand(ICommand command)
    {
        if (command is GetBaseAttributeScore getAttributeScore)
        {
            if (getAttributeScore.AttributeType == EAttributeType.Constitution)
            {
                getAttributeScore.AddBonus(this, 2);
            }
        }
    }
}
