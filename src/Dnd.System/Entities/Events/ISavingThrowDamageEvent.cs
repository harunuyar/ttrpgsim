namespace Dnd.System.Entities.Events;

using Dnd.System.Entities.Action.ActionTypes;

public interface ISavingThrowDamageEvent : IDamageEvent
{
    ISavingThrowAttackAction SavingThrowAttackAction { get; }
    bool Saved { get; }
}
