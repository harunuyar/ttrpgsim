namespace Dnd.Predefined.Effects.Conditions;

using Dnd._5eSRD.Constants;
using Dnd._5eSRD.Models.Condition;
using Dnd.Context;
using Dnd.Predefined.Commands.RollBonusCommands;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.GameActor;
using Dnd.System.GameManagers.Dice;

public class Invisible : AConditionEffect
{
    public static async Task<Invisible?> Create()
    {
        var conditionModel = await DndContext.Instance.GetObject<ConditionModel>(Conditions.Invisible);

        if (conditionModel == null)
        {
            return null;
        }

        return new Invisible(conditionModel);
    }

    private Invisible(ConditionModel conditionModel) : base(conditionModel)
    {
    }

    public override Task HandleCommand(ICommand command, IGameActor effectSource, IGameActor effectOwner)
    {
        if (command is GetAdvantage advantage)
        {
            if (advantage.Action is IAttackAction)
            {
                advantage.AddValue(EAdvantage.Advantage, Name);
            }
        }
        else if (command is GetAdvantageFromOpponent advantageFromOpponent)
        {
            if (advantageFromOpponent.Action is IAttackAction)
            {
                advantageFromOpponent.AddValue(EAdvantage.Disadvantage, Name);
            }
        }

        return Task.CompletedTask;
    }
}
