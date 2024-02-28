namespace Dnd.Predefined.Actions;

using Dnd.System.Entities.Action;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.Effect;
using Dnd.System.Entities.GameActor;

public class EffectAction : TargetingAction, IEffectAction
{
    public EffectAction(IGameActor actionOwner, string name, ActionDurationType actionDurationType, ActionRange range, TargetingType targetingType) : base(actionOwner, name, actionDurationType, range, targetingType)
    {
    }

    public EffectDuration Duration => throw new NotImplementedException();

    public IEffectDefinition EffectDefinition => throw new NotImplementedException();
}

