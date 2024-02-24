namespace Dnd.Predefined.Effects.Conditions;

using Dnd._5eSRD.Constants;
using Dnd._5eSRD.Models.Condition;
using Dnd.Context;
using Dnd.System.Entities.Effect;
using Dnd.System.Entities.GameActor;
using Dnd.System.Entities.Units;

public class Deafened : AConditionEffect
{
    public static async Task<Deafened?> Create(IGameActor source, IGameActor target, EffectDurationType durationType, TimeSpan? duration = null, int? maxTriggerCount = null, int? maxRestCount = null)
    {
        var conditionModel = await DndContext.Instance.GetObject<ConditionModel>(Conditions.Deafened);

        if (conditionModel == null)
        {
            return null;
        }

        return new Deafened(conditionModel, durationType, source, target, duration, maxTriggerCount, maxRestCount);
    }

    private Deafened(ConditionModel conditionModel, EffectDurationType durationType, IGameActor source, IGameActor target, TimeSpan? duration = null, int? maxTriggerCount = null, int? maxRestCount = null)
        : base(conditionModel, durationType, source, target, duration, maxTriggerCount, maxRestCount)
    {
    }
}
