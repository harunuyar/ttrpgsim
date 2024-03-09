namespace Dnd.System.Entities.Action.ActionTypes;

public interface IRerollAction : IReaction
{
    ERollType RollType { get; }
}
