namespace Dnd.System.Entities.Events;

using Dnd.System.CommandSystem.Results;
using Dnd.System.Entities.Action.ActionTypes;

public interface IAttackRollEvent : IAttackEvent
{
    IAttackRollAction AttackRollAction { get; }

    // Initialized in RunEvent method
    ScoreResult? ArmorClass { get; }
}
