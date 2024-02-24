namespace Dnd.Predefined.Effects.Conditions;

using Dnd._5eSRD.Constants;
using Dnd._5eSRD.Models.Condition;
using Dnd.Context;
using Dnd.Predefined.Commands.BonusCommands;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Effect;
using Dnd.System.Entities.GameActor;
using Dnd.System.Entities.Units;
using Dnd.System.GameManagers.Dice;

public class Poisoned : AConditionEffect
{
    public static async Task<Poisoned?> Create(IGameActor source, IGameActor target, EffectDurationType durationType, TimeSpan? duration = null, int? maxTriggerCount = null, int? maxRestCount = null)
    {
        var conditionModel = await DndContext.Instance.GetObject<ConditionModel>(Conditions.Poisoned);

        if (conditionModel == null)
        {
            return null;
        }

        return new Poisoned(conditionModel, durationType, source, target, duration, maxTriggerCount, maxRestCount);
    }

    private Poisoned(ConditionModel conditionModel, EffectDurationType durationType, IGameActor source, IGameActor target, TimeSpan? duration = null, int? maxTriggerCount = null, int? maxRestCount = null) 
        : base(conditionModel, durationType, source, target, duration, maxTriggerCount, maxRestCount)
    {
    }

    public override Task HandleCommand(ICommand command)
    {
        if (command is GetAdvantageForSavingThrow advantageForSavingThrow)
        {
            advantageForSavingThrow.AddValue(EAdvantage.Disadvantage, Name);
        }
        else if (command is GetAdvantageForAttackRoll advantageForAttackRoll)
        {
            advantageForAttackRoll.AddValue(EAdvantage.Disadvantage, Name);
        }

        return base.HandleCommand(command);
    }
}
