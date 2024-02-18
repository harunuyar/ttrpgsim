namespace Dnd.System.Entities.Actions;

using Dnd.System.Entities.Units;

public abstract class ARangeAreaAction : AAction
{
    public ARangeAreaAction(string name, string description, EActionType actionType, List<UsageLimitation> usageLimitations, Distance rangeDistance, Distance areaDistance) 
        : base(name, description, actionType, usageLimitations, new Range(ERange.RangeArea, rangeDistance, areaDistance))
    {
    }

    public virtual bool Use(IDndEntity actor, IEnumerable<IDndEntity> targets)
    {
        return Use();
    }
}
