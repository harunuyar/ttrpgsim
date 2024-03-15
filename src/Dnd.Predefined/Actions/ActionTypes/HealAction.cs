namespace Dnd.Predefined.Actions.ActionTypes;

using Dnd.Predefined.Events;
using Dnd.System.Entities.Action;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.Events;
using Dnd.System.Entities.GameActor;
using Dnd.System.GameManagers.Dice;

public class HealAction : EventAction, IHealAction
{
    public HealAction(string name, ActionDurationType actionDurationType, IEnumerable<IActionUsageLimit> usageLimits,
        ActionRange range, TargetingType targetingType, DicePool healDicePool)
        : base(name, actionDurationType, usageLimits)
    {
        AmountDicePool = healDicePool;
        AmountRollType = EAmountRollType.Healing;
        Range = range;
        TargetingType = targetingType;
    }

    public DicePool AmountDicePool { get; }

    public EAmountRollType AmountRollType { get; }

    public ActionRange Range { get; }

    public TargetingType TargetingType { get; }

    public override Task<IEvent> CreateEvent(IGameActor actor)
    {
        return Task.FromResult<IEvent>(new HealRollEvent(Name, actor, this, []));
    }
}
