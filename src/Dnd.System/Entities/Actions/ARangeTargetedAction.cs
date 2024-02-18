namespace Dnd.System.Entities.Actions;

using Dnd.System.Entities.Units;

public abstract class ARangeTargetedAction : AAction, ITargetedAction
{
    public ARangeTargetedAction(string name, string description, EActionType actionType, List<UsageLimitation> usageLimitations, Distance rangeDistance, int targetCount) 
        : base(name, description, actionType, usageLimitations, new Range(ERange.Range, rangeDistance, null))
    {
        TargetCount = targetCount;
    }

    public int TargetCount { get; }

    public virtual bool Use(IDndEntity actor, IEnumerable<IDndEntity> targets)
    {
        return Use();
    }
}
