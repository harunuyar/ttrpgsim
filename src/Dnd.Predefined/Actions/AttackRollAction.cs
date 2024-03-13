namespace Dnd.Predefined.Actions;

using Dnd._5eSRD.Models.DamageType;
using Dnd.Predefined.Events;
using Dnd.System.Entities.Action;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.Events;
using Dnd.System.Entities.GameActor;
using Dnd.System.GameManagers.Dice;

public class AttackRollAction : AttackAction, IAttackRollAction
{
    public AttackRollAction(string name, ActionDurationType actionDurationType, ActionRange range, TargetingType targetingType, DamageTypeModel damageType, DicePool damageDicePool, IEnumerable<IActionUsageLimit> usageLimits) 
        : base(name, actionDurationType, range, targetingType, damageType, damageDicePool, usageLimits)
    {
        SuccessRollAction = new SuccessRollAction(Name, actionDurationType, ESuccessRollType.Attack, []);
    }

    public ESuccessRollType SuccessRollType => SuccessRollAction.SuccessRollType;

    private SuccessRollAction SuccessRollAction { get; }

    public override Task<IEvent> CreateEvent(IGameActor actor)
    {
        if (TargetingType.TargetCount == 1)
        {
            return Task.FromResult<IEvent>(new AttackRollEvent(Name, actor, this, null));
        }
        else
        {
            return Task.FromResult<IEvent>(new MultipleAttackRollEvent(Name, actor, this, []));
        }
    }
}
