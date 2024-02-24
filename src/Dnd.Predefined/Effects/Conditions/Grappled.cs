namespace Dnd.Predefined.Effects.Conditions;

using Dnd._5eSRD.Constants;
using Dnd._5eSRD.Models.Condition;
using Dnd.Context;
using Dnd.Predefined.Commands.ScoreCommands;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Effect;
using Dnd.System.Entities.GameActor;
using Dnd.System.Entities.Units;

public class Grappled : AConditionEffect
{
    public static async Task<Grappled?> Create(IGameActor source, IGameActor target, EffectDurationType durationType, TimeSpan? duration = null, int? maxTriggerCount = null, int? maxRestCount = null)
    {
        var conditionModel = await DndContext.Instance.GetObject<ConditionModel>(Conditions.Grappled);

        if (conditionModel == null)
        {
            return null;
        }

        return new Grappled(conditionModel, durationType, source, target, duration, maxTriggerCount, maxRestCount);
    }

    private Grappled(ConditionModel conditionModel, EffectDurationType durationType, IGameActor source, IGameActor target, TimeSpan? duration = null, int? maxTriggerCount = null, int? maxRestCount = null) 
        : base(conditionModel, durationType, source, target, duration, maxTriggerCount, maxRestCount)
    {
    }

    public override Task HandleCommand(ICommand command)
    {
        if (command is GetSpeed getSpeed)
        {
            getSpeed.SetBaseValueAndReturn(0, Name);
        }

        return base.HandleCommand(command);
    }
}
