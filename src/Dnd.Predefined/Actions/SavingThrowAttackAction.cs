namespace Dnd.Predefined.Actions;

using Dnd._5eSRD.Models.AbilityScore;
using Dnd._5eSRD.Models.DamageType;
using Dnd.Predefined.Events;
using Dnd.System.Entities.Action;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.Events;
using Dnd.System.Entities.GameActor;
using Dnd.System.GameManagers.Dice;

public class SavingThrowAttackAction : AttackAction, ISavingThrowAttackAction
{
    public SavingThrowAttackAction(string name, AbilityScoreModel ability, ActionDurationType actionDurationType, ActionRange range, TargetingType targetingType, DamageTypeModel damageType, DicePool damageDicePool, IEnumerable<IActionUsageLimit> usageLimits) 
        : base(name, actionDurationType, range, targetingType, damageType, damageDicePool, usageLimits)
    {
        Ability = ability;
    }

    public AbilityScoreModel Ability { get; }

    public double SaveDamageMultiplier { get; }

    public ESuccessRollType SuccessRollType { get; }

    public override Task<IEvent> CreateEvent(IGameActor actor)
    {
        return Task.FromResult<IEvent>(new SavingThrowAttackEvent(Name, actor, this, []));
    }
}
