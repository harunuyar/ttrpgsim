﻿namespace Dnd.Entities.Traits.HalfElf;

using Dnd.CommandSystem.Commands;
using Dnd.CommandSystem.Commands.IntegerResultCommands;
using Dnd.Entities.Attributes;

public class AbilityScoreIncrease : ATrait
{
    public static AbilityScoreIncrease Instance(EAttributeType attribute1, EAttributeType attribute2) => new AbilityScoreIncrease(attribute1, attribute2);

    private AbilityScoreIncrease(EAttributeType attribute1, EAttributeType attribute2) : base("Ability Score Increase", "Your Charisma score increases by 2, and two other ability scores of your choice increase by 1.")
    {
        Attribute1 = attribute1;
        Attribute2 = attribute2;
    }

    public EAttributeType Attribute1 { get; }
    public EAttributeType Attribute2 { get; }

    public override void HandleCommand(DndCommand command)
    {
        if (command is GetAttributeScore getAttributeScore)
        {
            if (getAttributeScore.AttributeType == EAttributeType.Charisma)
            {
                getAttributeScore.Result.BonusCollection.AddBonus(this, 2);
            }
            else if (getAttributeScore.AttributeType == Attribute1)
            {
                getAttributeScore.Result.BonusCollection.AddBonus(this, 1);
            }
            else if (getAttributeScore.AttributeType == Attribute2)
            {
                getAttributeScore.Result.BonusCollection.AddBonus(this, 1);
            }
        }
    }
}