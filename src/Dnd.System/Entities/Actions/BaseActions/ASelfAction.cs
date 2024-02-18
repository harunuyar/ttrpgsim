namespace Dnd.System.Entities.Actions.BaseActions;

public abstract class ASelfAction : AAction
{
    public ASelfAction(string name, string description, EActionType actionType, List<UsageLimitation> usageLimitations)
        : base(name, description, actionType, new Range(ERange.Self, null, null), usageLimitations)
    {
    }
}
