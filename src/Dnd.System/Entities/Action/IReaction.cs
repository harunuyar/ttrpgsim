namespace Dnd.System.Entities.Action;

public interface IReaction : IAction
{
    EReactionType ReactionType { get; }
}
