namespace Dnd.Predefined.Actions;

using Dnd.System.Entities.Action;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.Effect;

public class EffectAction : TargetingAction, IEffectAction
{
    public EffectAction(string name, ActionDurationType actionDurationType, ActionRange range, TargetingType targetingType, IEffectDefinition effect, EffectDuration effectDuration, IEnumerable<IActionUsageLimit> usageLimits) 
        : base(name, actionDurationType, range, targetingType, usageLimits)
    {
        EffectDefinition = effect;
        Duration = effectDuration;
    }

    public EffectDuration Duration { get; }

    public IEffectDefinition EffectDefinition { get; }
}

