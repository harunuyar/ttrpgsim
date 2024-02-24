namespace Dnd.Predefined.Effects.Conditions;

using Dnd._5eSRD.Constants;
using Dnd._5eSRD.Models.Condition;
using Dnd.Context;
using Dnd.Predefined.Commands.BonusCommands;
using Dnd.Predefined.Commands.BoolCommands;
using Dnd.Predefined.Commands.ValueCommands;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Effect;
using Dnd.System.Entities.GameActor;
using Dnd.System.Entities.Units;
using Dnd.System.GameManagers.Dice;

public class Paralyzed : AConditionEffect
{
    public static async Task<Paralyzed?> Create(IGameActor source, IGameActor target, EffectDurationType durationType, TimeSpan? duration = null, int? maxTriggerCount = null, int? maxRestCount = null)
    {
        var conditionModel = await DndContext.Instance.GetObject<ConditionModel>(Conditions.Paralyzed);

        if (conditionModel == null)
        {
            return null;
        }

        return new Paralyzed(conditionModel, durationType, source, target, duration, maxTriggerCount, maxRestCount);
    }

    private Paralyzed(ConditionModel conditionModel, EffectDurationType durationType, IGameActor source, IGameActor target, TimeSpan? duration = null, int? maxTriggerCount = null, int? maxRestCount = null) 
        : base(conditionModel, durationType, source, target, duration, maxTriggerCount, maxRestCount)
    {
    }

    public override Task HandleCommand(ICommand command)
    {
        if (command is CanTakeAnyAction canTakeAnyAction)
        {
            canTakeAnyAction.SetValue(false, "You are paralyzed and can't take any action.");
        }
        else if (command is GetPreDeterminedSavingThrowResult savingThrowResult)
        {
            if (savingThrowResult.SavingThrowAction.Ability.Url == AbilityScores.Str || savingThrowResult.SavingThrowAction.Ability.Url == AbilityScores.Dex)
            {
                savingThrowResult.SetValue(ERollResult.Failure, Name);
            }
        }
        else if (command is GetAdvantageForAttackRollAgainst advantageForAttackRollAgainst)
        {
            advantageForAttackRollAgainst.AddValue(EAdvantage.Advantage, Name);
        }
        else if (command is GetRollSuccessAgainst rollSuccessAgainst)
        {
            if (rollSuccessAgainst.NormalResult == ERollResult.Success)
            {
                rollSuccessAgainst.SetValue(ERollResult.CriticalSuccess, Name);
            }
        }

        return base.HandleCommand(command);
    }
}
