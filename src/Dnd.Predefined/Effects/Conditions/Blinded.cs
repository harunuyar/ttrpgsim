namespace Dnd.Predefined.Effects.Conditions;

using Dnd._5eSRD.Constants;
using Dnd._5eSRD.Models.Condition;
using Dnd.Context;
using Dnd.Predefined.Commands.RollBonusCommands;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.Effect;
using Dnd.System.Entities.GameActor;
using Dnd.System.GameManagers.Dice;

public class Blinded : AConditionEffect
{
    public static async Task<Blinded?> Create(IGameActor source, IGameActor target, EffectDuration durationType)
    {
        var conditionModel = await DndContext.Instance.GetObject<ConditionModel>(Conditions.Blinded);

        if (conditionModel is null)
        {
            return null;
        }

        return new Blinded(conditionModel, durationType, source, target);
    }

    private Blinded(ConditionModel conditionModel, EffectDuration durationType, IGameActor source, IGameActor target) 
        : base(conditionModel, durationType, source, target)
    {
    }

    public override Task HandleCommand(ICommand command)
    {
        if (command is GetAdvantage advantage)
        {
            if (advantage.Action is IAttackAction)
            {
                advantage.AddValue(EAdvantage.Disadvantage, Name);
            }
        }
        else if (command is GetAdvantageFromOpponent advantageFromOpponent)
        {
            if (advantageFromOpponent.Action is IAttackAction)
            {
                advantageFromOpponent.AddValue(EAdvantage.Advantage, Name);
            }
        }

        return base.HandleCommand(command);
    }
}
