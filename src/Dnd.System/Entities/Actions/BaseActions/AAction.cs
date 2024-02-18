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

    public EActionType ActionType { get; set; }

    public List<UsageLimitation> UsageLimitations { get; }

    public Range Range { get; }

    public virtual bool IsAvailable(IGameActor gameActor)
    {
        if ((ActionType.HasFlag(EActionType.MainAction) && gameActor.ActionCounter.ActionPoints > 1)
            || (ActionType.HasFlag(EActionType.BonusAction) && gameActor.ActionCounter.BonusActionPoints > 1)
            || (ActionType.HasFlag(EActionType.Reaction) && gameActor.ActionCounter.ReactionPoints > 1)
            || ActionType.HasFlag(EActionType.FreeAction))
        {
            return UsageLimitations.All(x => x.IsAvailable());
        }

        return false;
    }

    public virtual void Use()
    {
        UsageLimitations.ForEach(x => x.Use());
    }

    public virtual void HandleCommand(ICommand command)
    {
        UsageLimitations.ForEach(x => x.HandleCommand(command));
    }

    public virtual void Apply(IGameActor actor, IEnumerable<IGameActor> targets)
    {
    }
}
