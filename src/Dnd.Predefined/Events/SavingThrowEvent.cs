namespace Dnd.Predefined.Events;

using Dnd.Predefined.Commands.ScoreCommands;
using Dnd.System.CommandSystem.Results;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.Events;
using Dnd.System.Entities.GameActor;

public abstract class SavingThrowEvent : SuccessRollAttackEvent
{
    public SavingThrowEvent(string name, IGameActor eventOwner, ISavingThrowAction savingThrowAction, IGameActor? target) 
        : base(name, eventOwner, target)
    {
        SavingThrowAction = savingThrowAction;
    }

    public ISavingThrowAction SavingThrowAction { get; }

    public override Task<ISuccessRollEvent> GetSuccessRollEvent(int targetResult, IGameActor opponent)
    {
        return Task.FromResult<ISuccessRollEvent>(new RollSuccessEvent($"{EventName}: Saving Throw", opponent, SavingThrowAction, targetResult, EventOwner));
    }

    public override Task<ScoreResult> GetTargetResult(IGameActor opponent)
    {
        return new GetSavingDC(EventOwner, SavingThrowAction).Execute();
    }
}
