namespace Dnd.System.Entities.Action.ActionTypes;

public interface IAttackRollAction : IAttackAction
{
    bool SneakAttack { get; }
}
