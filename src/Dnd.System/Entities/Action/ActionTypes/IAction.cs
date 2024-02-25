namespace Dnd.System.Entities.Action.ActionTypes;

using Dnd.System.Entities.Effect;
using Dnd.System.Entities.GameActor;

public interface IAction : ICommandHandler
{
    string Name { get; }
    IGameActor? ActionOwner { get; }
    ActionDurationType? ActionDuration { get; }
    ActionRange? Range { get; }
    EffectDuration? Duration { get; }
    TargetingType? TargetingType { get; }
    EReactionType? ReactionType { get; }
    bool Concentration { get; }
}
