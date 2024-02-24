namespace Dnd.Predefined.Effects;

using Dnd._5eSRD.Models.Condition;
using Dnd.System.Entities.Effect;
using Dnd.System.Entities.GameActor;
using Dnd.System.Entities.Units;

public abstract class AConditionEffect : AEffect
{
    public AConditionEffect(ConditionModel conditionModel, EffectDurationType durationType, IGameActor source, IGameActor target, TimeSpan? duration = null, int? maxTriggerCount = null, int? maxRestCount = null)
        : base(conditionModel.Name ?? "", string.Join(" ", conditionModel.Desc ?? []), durationType, source, target, duration, maxTriggerCount, maxRestCount)
    {
        ConditionModel = conditionModel;
    }

    public ConditionModel ConditionModel { get; }
}
