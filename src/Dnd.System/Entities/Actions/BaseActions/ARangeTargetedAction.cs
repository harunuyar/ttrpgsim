namespace Dnd.System.Entities.Actions.BaseActions;

using Dnd.System.Entities.Units;

public abstract class ARangeTargetedAction : AAction, ITargetedAction
{
    public ARangeTargetedAction(string name, string description, EActionType actionType, List<UsageLimitation> usageLimitations, Distance rangeDistance, int targetCount)
        : base(name, description, actionType, new Range(ERange.Range, rangeDistance, null), usageLimitations)
    {
        TargetCount = targetCount;
    }

    public int TargetCount { get; }
}
