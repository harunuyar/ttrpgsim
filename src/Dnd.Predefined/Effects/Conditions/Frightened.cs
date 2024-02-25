namespace Dnd.Predefined.Effects.Conditions;

using Dnd._5eSRD.Constants;
using Dnd._5eSRD.Models.Condition;
using Dnd.Context;
using Dnd.Predefined.Commands.BonusCommands;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Effect;
using Dnd.System.Entities.GameActor;
using Dnd.System.GameManagers.Dice;

public class Frightened : AConditionEffect
{
    public static async Task<Frightened?> Create(IGameActor source, IGameActor target, EffectDuration durationType)
    {
        var conditionModel = await DndContext.Instance.GetObject<ConditionModel>(Conditions.Frightened);

        if (conditionModel == null)
        {
            return null;
        }

        return new Frightened(conditionModel, durationType, source, target);
    }

    private Frightened(ConditionModel conditionModel, EffectDuration durationType, IGameActor source, IGameActor target) 
        : base(conditionModel, durationType, source, target)
    {
    }

    public override Task HandleCommand(ICommand command)
    {
        if (command is GetAdvantageForAttackRoll advantageForAttackRoll)
        {
            advantageForAttackRoll.AddValue(EAdvantage.Disadvantage, Name);
        }
        else if (command is GetAdvantageForAttackRollAgainst advantageForAttackRollAgainst)
        {
            advantageForAttackRollAgainst.AddValue(EAdvantage.Advantage, Name);
        }

        return base.HandleCommand(command);
    }
}
