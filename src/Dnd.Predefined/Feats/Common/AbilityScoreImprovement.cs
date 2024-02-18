namespace Dnd.Predefined.Feats.Common;

using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.CommandSystem.Commands.IntegerResultCommands;
using Dnd.System.Entities.Attributes;
using Dnd.System.Entities.GameActors;

public class AbilityScoreImprovement : AFeat
{
    public AbilityScoreImprovement(EAttributeType attribute1, EAttributeType attribute2)
        : base("Ability Score Improvement",
            "You can increase one ability score of your choice by 2, or you can increase two ability scores of your choice by 1. You can’t increase an ability score above 20 using this feature.")
    {
        Attribute1 = attribute1;
        Attribute2 = attribute2;
    }

    public EAttributeType Attribute1 { get; }

    public EAttributeType Attribute2 { get; }

    public override bool IsEligible(IGameActor actor)
    {
        var getAttributeScore1 = new GetBaseAttributeScore(actor, Attribute1).Execute();

        if (!getAttributeScore1.IsSuccess)
        {
            return false;
        }

        if (Attribute1 == Attribute2)
        {
            return getAttributeScore1.Value + 2 <= 20;
        }

        if (getAttributeScore1.Value + 1 > 20)
        {
            return false;
        }

        var getAttributeScore2 = new GetBaseAttributeScore(actor, Attribute2).Execute();

        if (!getAttributeScore2.IsSuccess)
        {
            return false;
        }

        return getAttributeScore2.Value + 1 <= 20;
    }

    public override void HandleCommand(ICommand command)
    {
        if (command is GetBaseAttributeScore getBaseAttributeScore)
        {
            if (getBaseAttributeScore.AttributeType == Attribute1)
            {
                if (Attribute1 == Attribute2)
                {
                    getBaseAttributeScore.AddBonus(this, 2);
                }
                else
                {
                    getBaseAttributeScore.AddBonus(this, 1);
                }
            }
            else if (getBaseAttributeScore.AttributeType == Attribute1)
            {
                getBaseAttributeScore.AddBonus(this, 1);
            }
        }
    }
}
