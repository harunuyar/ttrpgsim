namespace Dnd.Predefined.Actions;

using Dnd.Predefined.Commands.BoolCommands;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action;
using Dnd.System.Entities.Action.ActionTypes;

public class Action : IAction
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

    public virtual Task HandleCommand(ICommand command)
    {
        return Task.CompletedTask;
    }

    public virtual async Task HandleUsageCommand(ICommand command)
    {
        if (command is IsActionAvailable isActionAvailable)
        {
            if (UsageLimits.Any(x => x.IsExhausted))
            {
                isActionAvailable.SetValue(false, "No uses left");
            }
        }

        foreach (var usageLimit in UsageLimits)
        {
            await usageLimit.HandleCommand(command);
        }
    }
}
