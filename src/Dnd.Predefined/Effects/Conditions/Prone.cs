namespace Dnd.Predefined.Effects.Conditions;

using Dnd._5eSRD.Constants;
using Dnd._5eSRD.Models.Condition;
using Dnd.Context;
using Dnd.System.Entities.Effect;
using Dnd.System.Entities.GameActor;

public class Prone : AConditionEffect
{
    public static async Task<Prone?> Create(IGameActor source, IGameActor target, EffectDuration durationType)
    {
        var conditionModel = await DndContext.Instance.GetObject<ConditionModel>(Conditions.Poisoned);

        if (conditionModel == null)
        {
            return null;
        }

        return new Prone(conditionModel, durationType, source, target);
    }

    private Prone(ConditionModel conditionModel, EffectDuration durationType, IGameActor source, IGameActor target) 
        : base(conditionModel, durationType, source, target)
    {
    }
}
