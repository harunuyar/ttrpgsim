namespace Dnd.System.Entities.Action.ActionTypes;

using Dnd.System.Entities.Events;
using Dnd.System.Entities.GameActor;

public interface IAction : ICommandHandler, IUsageBonusProvider
{
    string Name { get; }
    ActionDurationType ActionDuration { get; }
    List<IActionUsageLimit> UsageLimits { get; }
    Task<bool> IsActionAvailable(IGameActor gameActor);
    IActionEvent CreateEvent(IGameActor gameActor);
    void MarkUse();
}
