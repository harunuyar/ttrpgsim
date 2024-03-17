namespace Dnd.Predefined.Effects.Conditions;

using Dnd._5eSRD.Constants;
using Dnd._5eSRD.Models.Condition;
using Dnd.Context;
using Dnd.Predefined.Commands.BoolCommands;
using Dnd.Predefined.Commands.RollBonusCommands;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.GameActor;
using Dnd.System.GameManagers.Dice;

public class Paralyzed : AConditionEffect
{
    public static async Task<Paralyzed?> Create()
    {
        var conditionModel = await DndContext.Instance.GetObject<ConditionModel>(Conditions.Paralyzed);

        if (conditionModel == null)
        {
            return null;
        }

        return new Paralyzed(conditionModel);
    }

    private Paralyzed(ConditionModel conditionModel) : base(conditionModel)
    {
    }

    public override Task HandleCommand(ICommand command, IGameActor effectSource, IGameActor effectOwner)
    {
        if (command is CanTakeAnyAction canTakeAnyAction)
        {
            canTakeAnyAction.SetValue(false, "You are paralyzed and can't take any action.");
        }
        else if (command is GetPredeterminedRollResult predeterminedRollResult)
        {
            if (predeterminedRollResult.Action is ISavingThrowAction savingThrow && (savingThrow.Ability.Url == AbilityScores.Str || savingThrow.Ability.Url == AbilityScores.Dex))
            {
                predeterminedRollResult.SetValue(ERollResult.Failure, Name);
            }
        }
        else if (command is GetAdvantageFromOpponent advantageFromOpponent)
        {
            if (advantageFromOpponent.Action is IAttackAction)
            {
                advantageFromOpponent.AddValue(EAdvantage.Advantage, Name);
            }
        }
        else if (command is GetPostDeterminedResultFromOpponent actionResultFromOpponent)
        {
            if (actionResultFromOpponent.Action is IAttackAction && actionResultFromOpponent.DefaultRollResult == ERollResult.Success)
            {
                actionResultFromOpponent.SetValue(ERollResult.CriticalSuccess, Name);
            }
        }

        return Task.CompletedTask;
    }
}
