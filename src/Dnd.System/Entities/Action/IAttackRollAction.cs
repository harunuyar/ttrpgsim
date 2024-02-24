namespace Dnd.System.Entities.Action;

public interface IAttackRollAction : IAttackAction
{
    bool SneakAttack { get; }
}
