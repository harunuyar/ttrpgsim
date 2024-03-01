namespace Dnd.Predefined.Actions;

using Dnd.Predefined.Commands.BoolCommands;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.GameActor;

public class Action : IAction
{
    public Action(IGameActor actionOwner, string name, ActionDurationType actionDurationType)
    {
        ActionOwner = actionOwner;
        Name = name;
        ActionDuration = actionDurationType;
        ReactionType = EReactionType.None;
    }

    public IGameActor ActionOwner { get; }

    public string Name { get; }

    public ActionDurationType ActionDuration { get; }

    public EReactionType ReactionType { get; set; }

    public virtual Task HandleCommand(ICommand command)
    {
        return Task.CompletedTask;
    }

    public virtual Task HandleUsageCommand(ICommand command)
    {
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
