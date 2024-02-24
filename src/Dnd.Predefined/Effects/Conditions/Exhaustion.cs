namespace Dnd.Predefined.Effects.Conditions;

using Dnd._5eSRD.Constants;
using Dnd._5eSRD.Models.Condition;
using Dnd.Context;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Effect;
using Dnd.System.Entities.GameActor;
using Dnd.System.Entities.Units;

public class Exhaustion : AConditionEffect
{
    public static async Task<Exhaustion?> Create(IGameActor source, IGameActor target, int level, EffectDurationType durationType, TimeSpan? duration = null, int? maxTriggerCount = null, int? maxRestCount = null)
    {
        var conditionModel = await DndContext.Instance.GetObject<ConditionModel>(Conditions.Exhaustion);

        if (conditionModel == null)
        {
            return null;
        }

        return new Exhaustion(conditionModel, durationType, source, target, level, duration, maxTriggerCount, maxRestCount);
    }

    private Exhaustion(ConditionModel conditionModel, EffectDurationType durationType, IGameActor source, IGameActor target, int level, TimeSpan? duration = null, int? maxTriggerCount = null, int? maxRestCount = null)
        : base(conditionModel, durationType, source, target, duration, maxTriggerCount, maxRestCount)
    {
        Level = level;
    }

    public int Level { get; set; }

    public override Task HandleCommand(ICommand command)
    {
        // TODO: Implement exhaustion effects
        return base.HandleCommand(command);
    }
}