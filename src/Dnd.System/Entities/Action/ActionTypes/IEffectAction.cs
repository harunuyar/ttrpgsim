namespace Dnd.System.Entities.Action.ActionTypes;

using Dnd.System.Entities.Effect;

public interface IEffectAction : ITargetingAction
{
    IEffectDefinition EffectDefinition { get; }
    EffectDuration Duration { get; }
}
