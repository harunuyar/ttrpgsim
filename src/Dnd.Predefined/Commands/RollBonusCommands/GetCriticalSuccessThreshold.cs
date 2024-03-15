namespace Dnd.Predefined.Commands.RollBonusCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.GameActor;

public class GetCriticalSuccessThreshold : ScoreCommand
{
    public GetCriticalSuccessThreshold(IGameActor actor, ISuccessRollAction action) : base(actor)
    {
        Action = action;
    }

    public ISuccessRollAction Action { get; }

    protected override Task InitializeResult()
    {
        SetBaseValue(20, "Base");
        return Task.CompletedTask;
    }
}
