namespace Dnd.System.Entities.Actions.BaseActions;

using Dnd.System.Entities.Units;

public abstract class ASelfAreaAction : AAction
{
    public ASelfAreaAction(string name, string description, EActionType actionType, List<UsageLimitation> usageLimitations, Distance areaDistance)
        : base(name, description, actionType, new Range(ERange.SelfArea, null, areaDistance), usageLimitations)
    {
    }
}
