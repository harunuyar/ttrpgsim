namespace Dnd.Predefined.Effects.Conditions;

using Dnd._5eSRD.Constants;
using Dnd._5eSRD.Models.Condition;
using Dnd.Context;
using Dnd.Predefined.Commands.BoolCommands;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Effect;
using Dnd.System.Entities.GameActor;
using Dnd.System.Entities.Units;

public class Incapacitated : AConditionEffect
{
    public static async Task<Incapacitated?> Create(IGameActor source, IGameActor target, EffectDurationType durationType, TimeSpan? duration = null, int? maxTriggerCount = null, int? maxRestCount = null)
    {
        var conditionModel = await DndContext.Instance.GetObject<ConditionModel>(Conditions.Incapacitated);

        if (conditionModel == null)
        {
            return null;
        }

        return new Incapacitated(conditionModel, durationType, source, target, duration, maxTriggerCount, maxRestCount);
    }

    private Incapacitated(ConditionModel conditionModel, EffectDurationType durationType, IGameActor source, IGameActor target, TimeSpan? duration = null, int? maxTriggerCount = null, int? maxRestCount = null) 
        : base(conditionModel, durationType, source, target, duration, maxTriggerCount, maxRestCount)
    {
    }

    public override Task HandleCommand(ICommand command)
    {
        if (command is CanTakeAnyAction canTakeAnyAction)
        {
            canTakeAnyAction.SetValue(false, "You can't take any action or reaction while incapacitated.");
        }

        return base.HandleCommand(command);
    }
}
