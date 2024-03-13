namespace Dnd.System.Entities.Action.ActionTypes;

public interface ISavingThrowAttackAction : IAttackAction, ISavingThrowAction
{
    double SaveDamageMultiplier { get; }
}
