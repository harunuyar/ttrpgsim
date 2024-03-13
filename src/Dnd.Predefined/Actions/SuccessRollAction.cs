namespace Dnd.Predefined.Actions;

using Dnd.Predefined.Events;
using Dnd.System.Entities.Action;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.Events;
using Dnd.System.Entities.GameActor;

public class SuccessRollAction : Action, ISuccessRollAction
{
    public SuccessRollAction(string name, ActionDurationType actionDurationType, ESuccessRollType successRollType, IEnumerable<IActionUsageLimit> usageLimits)
        : base(name, actionDurationType, usageLimits)
    {
        SuccessRollType = successRollType;
    }

    public ESuccessRollType SuccessRollType { get; }

    public override Task<IEvent> CreateEvent(IGameActor actor)
    {
        return Task.FromResult<IEvent>(new SuccessRollEvent(Name, actor, this, null, null));
    }
}
