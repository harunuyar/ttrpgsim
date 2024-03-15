namespace Dnd.Predefined.Events;

using Dnd.Predefined.Commands.ScoreCommands;
using Dnd.System.CommandSystem.Results;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.Events;
using Dnd.System.Entities.GameActor;

public abstract class AttackRollEvent : SuccessRollAttackEvent
{
    public AttackRollEvent(string name, IGameActor eventOwner, IAttackRollAction action, IGameActor? target)
        : base(name, eventOwner, target)
    {
        AttackRollAction = action;
    }

    public IAttackRollAction AttackRollAction { get; }

    public ScoreResult? ArmorClass { get; private set; }

    public override Task<ISuccessRollEvent> GetSuccessRollEvent(int targetResult, IGameActor opponent)
    {
        return Task.FromResult<ISuccessRollEvent>(new RollSuccessEvent($"{EventName}: Success Roll", EventOwner, AttackRollAction, targetResult, opponent));
    }

    public override Task<ScoreResult> GetTargetResult(IGameActor opponent)
    {
        return new GetArmorClass(opponent).Execute();
    }
}
