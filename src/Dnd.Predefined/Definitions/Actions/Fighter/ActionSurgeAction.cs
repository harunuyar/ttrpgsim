namespace Dnd.Predefined.Definitions.Actions.Fighter;

using Dnd.Predefined.Actions;
using Dnd.Predefined.Events;
using Dnd.System.Entities.Action;
using Dnd.System.Entities.Events;
using Dnd.System.Entities.GameActor;

public class ActionSurgeAction : Action
{
    public ActionSurgeAction(IActionUsageLimit usageLimit) : base("Action Surge", ActionDurationType.FreeAction, [usageLimit])
    {
    }

    public override IActionEvent CreateEvent(IGameActor actor)
    {
        var task = new Task(() => 
        {
            actor.PointsCounter.AddExtraActionPoint();
        });

        return new BasicActionEvent(actor, this, task);
    }
}
