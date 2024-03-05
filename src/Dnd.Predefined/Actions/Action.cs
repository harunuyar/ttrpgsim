namespace Dnd.Predefined.Actions;

using Dnd.Predefined.Commands.BoolCommands;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.GameActor;

public class Action : IAction
{
    public Action(IGameActor actionOwner, string name, ActionDurationType actionDurationType, IEnumerable<IActionUsageLimit> usageLimits)
    {
        ActionOwner = actionOwner;
        Name = name;
        ActionDuration = actionDurationType;
        UsageLimits = usageLimits.ToList();
    }

    public IGameActor ActionOwner { get; }

    public string Name { get; }

    public ActionDurationType ActionDuration { get; }

    public List<IActionUsageLimit> UsageLimits { get; }

    public virtual Task HandleCommand(ICommand command)
    {
        return Task.CompletedTask;
    }

    public virtual Task HandleUsageCommand(ICommand command)
    {
        if (command is IsActionAvailable isActionAvailable)
        {
            if (UsageLimits.Any(x => x.IsExhausted))
            {
                isActionAvailable.SetValue(false, "No uses left");
            }
        }

        return Task.CompletedTask;
    }

    public virtual async Task<bool> IsAvailable()
    {
        var isAvailable = await new IsActionAvailable(ActionOwner, this, null).Execute();

        if (!isAvailable.IsSuccess)
        {
            throw new InvalidOperationException("IsActionAvailable: " + isAvailable.ErrorMessage);
        }

        return isAvailable.Value;
    }
}
