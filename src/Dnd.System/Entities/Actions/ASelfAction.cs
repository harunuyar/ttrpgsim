namespace Dnd.System.Entities.Actions;

using Dnd.System.Entities.GameActors;

public abstract class ASelfAction : AAction
{
    public ASelfAction(string name, string description, EActionType actionType, List<UsageLimitation> usageLimitations) 
        : base(name, description, actionType, usageLimitations, new Range(ERange.Self, null, null))
    {
    }

    public virtual bool Use(IGameActor actor)
    {
        return Use();
    }
}
