namespace Dnd.System.Entities.Actions.BaseActions;

using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.Entities.GameActors;

public abstract class AAction : IAction
{
    public AAction(string name, string description, EActionType actionType, Range range, List<UsageLimitation> usageLimitations)
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

    public bool Use()
    {
        if (!IsAvailable)
        {
            return false;
        }

        UsageLimitations.ForEach(x => x.Use());

        return true;
    }

    public virtual void HandleCommand(ICommand command)
    {
        UsageLimitations.ForEach(x => x.HandleCommand(command));
    }

    public virtual void Apply(IGameActor actor, IEnumerable<IGameActor> targets)
    {
    }
}
