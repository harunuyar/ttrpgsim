namespace Dnd.System.Entities.Action.ActionTypes;

using Dnd.System.Entities.Action;
using Dnd.System.Entities.Effect;

public interface IApplyEffectAction : IAction
{
    IEffectDefinition EffectDefinition { get; }
    EffectDuration Duration { get; }
}
