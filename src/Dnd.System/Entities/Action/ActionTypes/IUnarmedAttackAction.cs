namespace Dnd.System.Entities.Action.ActionTypes;

public interface IUnarmedAttackAction : IAttackAction, ISuccessRollAction
{
    EAttackHandType HandType { get; }
}
