namespace Dnd.Predefined.Actions.Classes.Fighter;

using Dnd.System.Entities.Actions;
using Dnd.System.Entities.Actions.BaseActions;
using Dnd.System.Entities.GameActors;

public class ActionSurge : ASelfAction
{
    public ActionSurge(int level) 
        : base(
            "Action Surge", 
            "On your turn, you can take one additional action.", 
            EActionType.FreeAction, 
            [
                new UsageLimitation(EUsageLimitation.ShortRest | EUsageLimitation.LongRest, level),
                new UsageLimitation(EUsageLimitation.PerTurn, 1)
            ])
    {
        Level = level;
    }

    public int Level { get; }

    public override void Apply(IGameActor actor, IEnumerable<IGameActor> targets)
    {
        foreach (var target in targets)
        {
            target.ActionCounter.AddExtraActionPoint();
        }
    }
}
