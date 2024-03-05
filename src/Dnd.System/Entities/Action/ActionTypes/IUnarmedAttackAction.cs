namespace Dnd.System.Entities.Action.ActionTypes;

public interface IUnarmedAttackAction : IAttackRollAction
{
    EAttackHandType HandType { get; }
}
