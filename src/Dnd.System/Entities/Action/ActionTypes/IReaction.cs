namespace Dnd.System.Entities.Action.ActionTypes;

public interface IReaction : IAction
{
    EReactionType ReactionType { get; }
    bool MandatoryReaction { get; }
}
