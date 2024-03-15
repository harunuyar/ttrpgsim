namespace Dnd.Predefined.Actions.ActionTypes;

using Dnd._5eSRD.Models.Spell;
using Dnd.Predefined.Events;
using Dnd.System.Entities.Action;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.Events;
using Dnd.System.Entities.GameActor;
using Dnd.System.Entities.Instances;
using Dnd.System.GameManagers.Dice;

public class SpellHealAction : SpellEventAction, IHealAction
{
    public SpellHealAction(ISpellcastingAbility spellcastingAbility, SpellModel spellModel, int spellSlot, IEnumerable<IActionUsageLimit> usageLimits,
        TargetingType targetingType, DicePool healDicePool) 
        : base(spellcastingAbility, spellModel, spellSlot, usageLimits)
    {
        Range = ActionRange.FromString(spellModel.Range) ?? ActionRange.Self;
        TargetingType = targetingType;
        AmountDicePool = healDicePool;
    }

    public DicePool AmountDicePool { get; }

    public EAmountRollType AmountRollType => EAmountRollType.Healing;

    public ActionRange Range { get; }

    public TargetingType TargetingType { get; }

    public override Task<IEvent> CreateEvent(IGameActor actor)
    {
        return Task.FromResult<IEvent>(new HealRollEvent(Name, actor, this, []));
    }
}
