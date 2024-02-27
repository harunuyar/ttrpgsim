namespace Dnd.System.Entities.Action.ActionTypes;

using Dnd.System.Entities.Effect;

public interface IEffectAction : ITargetingAction
{
    EffectDuration Duration { get; }
}
