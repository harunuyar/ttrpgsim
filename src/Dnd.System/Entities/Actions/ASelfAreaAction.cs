namespace Dnd.System.Entities.Actions;

using Dnd.System.Entities.Units;

public abstract class ASelfAreaAction : AAction
{
    public ASelfAreaAction(string name, string description, EActionType actionType, List<UsageLimitation> usageLimitations, Distance areaDistance) 
        : base(name, description, actionType, usageLimitations, new Range(ERange.SelfArea, null, areaDistance))
    {
    }

    public virtual bool Use(IDndEntity actor, IEnumerable<IDndEntity> targets)
    {
        return Use();
    }
}
