namespace Dnd.System.Entities.Action.ActionTypes;

public interface ISavingThrowDamageAction : IDamageAction, ISavingThrowAction
{
    double SuccessDamageMultiplier { get; }
    double FailureDamageMultiplier { get; }
}
