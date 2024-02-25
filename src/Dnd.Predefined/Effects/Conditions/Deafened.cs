namespace Dnd.Predefined.Effects.Conditions;

using Dnd._5eSRD.Constants;
using Dnd._5eSRD.Models.Condition;
using Dnd.Context;
using Dnd.System.Entities.Effect;
using Dnd.System.Entities.GameActor;

public class Deafened : AConditionEffect
{
    public static async Task<Deafened?> Create(IGameActor source, IGameActor target, EffectDuration durationType)
    {
        var conditionModel = await DndContext.Instance.GetObject<ConditionModel>(Conditions.Deafened);

        if (conditionModel == null)
        {
            return null;
        }

        return new Deafened(conditionModel, durationType, source, target);
    }

    private Deafened(ConditionModel conditionModel, EffectDuration durationType, IGameActor source, IGameActor target)
        : base(conditionModel, durationType, source, target)
    {
    }
}
