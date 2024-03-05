namespace Dnd.System.Entities.Action.ActionTypes;

public interface IAction : ICommandHandler, IUsageBonusProvider
{
    string Name { get; }
    ActionDurationType ActionDuration { get; }
    List<IActionUsageLimit> UsageLimits { get; }
}
