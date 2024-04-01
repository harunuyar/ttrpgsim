namespace Dnd.System.Entities.Action.ActionTypes;

public interface IUnarmedAttackAction : IAttackRollAction, IEventAction
{
    EAttackHandType HandType { get; }
}
