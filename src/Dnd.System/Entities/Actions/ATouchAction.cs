namespace Dnd.System.Entities.Actions;

public abstract class ATouchAction : AAction, ITargetedAction
{
    public ATouchAction(string name, string description, EActionType actionType, List<UsageLimitation> usageLimitations) 
        : base(name, description, actionType, usageLimitations, new Range(ERange.Touch, null, null))
    {
    }

    public int TargetCount => 1;

    public virtual bool Use(IDndEntity actor, IDndEntity target)
    {
        return Use();
    }
}
