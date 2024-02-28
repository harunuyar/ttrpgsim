namespace Dnd.Predefined.Effects.Conditions;

using Dnd._5eSRD.Constants;
using Dnd._5eSRD.Models.Condition;
using Dnd.Context;
using Dnd.Predefined.Commands.RollBonusCommands;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.GameActor;
using Dnd.System.GameManagers.Dice;

public class Frightened : AConditionEffect
{
    public static async Task<Frightened?> Create()
    {
        var conditionModel = await DndContext.Instance.GetObject<ConditionModel>(Conditions.Frightened);

        if (conditionModel == null)
        {
            return null;
        }

        return new Frightened(conditionModel);
    }

    private Frightened(ConditionModel conditionModel) : base(conditionModel)
    {
    }

    public override Task HandleCommand(ICommand command, IGameActor effectSource, IGameActor effectOwner)
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

        return Task.CompletedTask;
    }
}
