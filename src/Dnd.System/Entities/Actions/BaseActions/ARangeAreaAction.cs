namespace Dnd.System.Entities.Actions.BaseActions;

using Dnd.System.Entities.Units;

public abstract class ARangeAreaAction : AAction
{
    public ARangeAreaAction(string name, string description, EActionType actionType, List<UsageLimitation> usageLimitations, Distance rangeDistance, Distance areaDistance)
        : base(name, description, actionType, new Range(ERange.RangeArea, rangeDistance, areaDistance), usageLimitations)
    {
    }
}
