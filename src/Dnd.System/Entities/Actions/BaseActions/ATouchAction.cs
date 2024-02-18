namespace Dnd.System.Entities.Actions.BaseActions;

public abstract class ATouchAction : AAction, ITargetedAction
{
    public ATouchAction(string name, string description, EActionType actionType, List<UsageLimitation> usageLimitations)
        : base(name, description, actionType, new Range(ERange.Touch, null, null), usageLimitations)
    {
    }

    public int TargetCount => 1;
}
