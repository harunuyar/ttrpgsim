namespace Dnd.Predefined.Actions.Classes.Fighter;

using Dnd.System.CommandSystem.Commands.EventCommands;
using Dnd.System.Entities.Actions;
using Dnd.System.Entities.Actions.BaseActions;
using Dnd.System.Entities.GameActors;
using Dnd.System.Events.EventListener;

public class SecondWindAction : ASelfAction
{
    public SecondWindAction() 
        : base(
            "Second Wind", 
            "You can use a bonus action to regain hit points equal to 1d10 + your fighter level. Once per short/long rest.", 
            EActionType.BonusAction, 
            [new UsageLimitation(EUsageLimitation.ShortRest | EUsageLimitation.LongRest, 1)])
    {
    }

    public override void Apply(IGameActor actor, IEnumerable<IGameActor> targets)
    {
        int amount = new Random().Next(1, 11) + actor.LevelInfo.GetLevelsInClass(new Predefined.Classes.Fighter());
        new ApplyHeal(new NoEventListener(), actor, amount).Execute();
    }
}
