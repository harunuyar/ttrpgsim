namespace Dnd.System.Entities.Action;

using Dnd.System.Entities.GameActor;

public interface IAction : ICommandHandler, IUsageBonusProvider
{
    string Name { get; }
    ActionDurationType ActionDuration { get; }
    List<IActionUsageLimit> UsageLimits { get; }
    Task<bool> IsActionAvailable(IGameActor gameActor);
    void MarkUse();
}
