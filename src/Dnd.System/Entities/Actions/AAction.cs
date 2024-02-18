namespace Dnd.System.Entities.Actions;

using Dnd.System.CommandSystem.Commands.BaseCommands;

public abstract class AAction : IAction
{
    public AAction(string name, string description, EActionType actionType, List<UsageLimitation> usageLimitations, Range range)
    {
        Name = name;
        Description = description;
        ActionType = actionType;
        UsageLimitations = usageLimitations;
        Range = range;
    }

    public string Name { get; }

    public string Description { get; }

    public EActionType ActionType { get; }

    public List<UsageLimitation> UsageLimitations { get; }

    public Range Range { get; }

    public bool IsAvailable => UsageLimitations.All(x => x.IsAvailable());

    public virtual void HandleCommand(ICommand command)
    {
        UsageLimitations.ForEach(x => x.HandleCommand(command));
    }

    public bool Use()
    {
        if (!IsAvailable)
        {
            return false;
        }

        UsageLimitations.ForEach(x => x.Use());

        return true;
    }
}
