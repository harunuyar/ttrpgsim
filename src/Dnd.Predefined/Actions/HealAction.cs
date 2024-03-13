namespace Dnd.Predefined.Actions;

using Dnd.Predefined.Events;
using Dnd.System.Entities.Action;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.Events;
using Dnd.System.Entities.GameActor;
using Dnd.System.GameManagers.Dice;

public class HealAction : TargetingAction, IHealAction
{
    public HealAction(string name, ActionDurationType actionDurationType, ActionRange range, TargetingType targetingType, DicePool healDicePool, IEnumerable<IActionUsageLimit> usageLimits)
        : base(name, actionDurationType, range, targetingType, usageLimits)
    {
        AmountAction = new AmountAction(name, actionDurationType, healDicePool, EAmountRollType.Healing, []);
    }

    public AmountAction AmountAction { get; }

    public DicePool AmountDicePool => AmountAction.AmountDicePool;

    public EAmountRollType AmountRollType => AmountAction.AmountRollType;

    public override Task<IEvent> CreateEvent(IGameActor actor)
    {
        return Task.FromResult<IEvent>(new HealRollEvent(Name, actor, this, []));
    }
}
