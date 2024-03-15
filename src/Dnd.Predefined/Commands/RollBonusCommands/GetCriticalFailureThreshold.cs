namespace Dnd.Predefined.Commands.RollBonusCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.GameActor;

public class GetCriticalFailureThreshold : ScoreCommand
{
    public GetCriticalFailureThreshold(IGameActor actor, ISuccessRollAction successRollAction) : base(actor)
    {
        Action = successRollAction;
    }

    public ISuccessRollAction Action { get; }

    protected override Task InitializeResult()
    {
        SetBaseValue(1, "Base");
        return Task.CompletedTask;
    }
}
