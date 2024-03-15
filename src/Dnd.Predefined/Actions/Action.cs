namespace Dnd.Predefined.Actions;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action;
using Dnd.System.Entities.GameActor;

public abstract class Action : IAction
{
    public Action(string name, ActionDurationType actionDurationType, IEnumerable<IActionUsageLimit> usageLimits)
    {
        Name = name;
        ActionDuration = actionDurationType;
        UsageLimits = usageLimits.ToList();
    }

    public string Name { get; }

    public ActionDurationType ActionDuration { get; }

    public List<IActionUsageLimit> UsageLimits { get; }

    public virtual Task<bool> IsActionAvailable(IGameActor gameActor)
    {
        return Task.FromResult(UsageLimits.All(x => !x.IsExhausted));
    }

    public void MarkUse()
    {
        foreach (var usageLimit in UsageLimits)
        {
            usageLimit.Use();
        }
    }

    public virtual async Task HandleCommand(ICommand command)
    {
        foreach (var usageLimit in UsageLimits)
        {
            await usageLimit.HandleCommand(command);
        }
    }

    public virtual Task HandleUsageCommand(ICommand command)
    {
        return Task.CompletedTask;
    }
}
