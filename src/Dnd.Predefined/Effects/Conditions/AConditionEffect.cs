namespace Dnd.Predefined.Effects.Conditions;

using Dnd._5eSRD.Models.Condition;
using Dnd.System.Entities.Effect;
using Dnd.System.Entities.GameActor;

public abstract class AConditionEffect : APersonalEffect
{
    public AConditionEffect(ConditionModel conditionModel, EffectDuration effectDuration, IGameActor source, IGameActor target)
        : base(conditionModel.Name ?? "", string.Join(" ", conditionModel.Desc ?? []), effectDuration, source, target)
    {
        ConditionModel = conditionModel;
    }

    public ConditionModel ConditionModel { get; }
}
