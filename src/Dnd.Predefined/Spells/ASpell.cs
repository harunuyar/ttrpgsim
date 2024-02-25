namespace Dnd.Predefined.Spells;

using Dnd._5eSRD.Models.Spell;
using Dnd.Predefined.ModelExtensions;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.Effect;
using Dnd.System.Entities.GameActor;

public abstract class ASpell : ISpellAction
{
    public ASpell(IGameActor? actionOwner, SpellModel spell, TargetingType? targetingType, EReactionType? reactionType = EReactionType.None)
    {
        ActionOwner = actionOwner;
        Spell = spell;
        TargetingType = targetingType;
        ReactionType = reactionType;
    }

    public SpellModel Spell { get; }

    public IGameActor? ActionOwner { get; }

    public ActionDurationType? ActionDuration => Spell.GetSpellActionDuration();

    public ActionRange? Range => Spell.GetSpellRange();

    public EffectDuration? Duration => Spell.GetSpellEffectDuration();

    public EReactionType? ReactionType { get; }

    public TargetingType? TargetingType { get; }

    public string Name => Spell.Name ?? string.Empty;

    public bool Concentration => Spell.Concentration ?? false;

    public virtual Task HandleCommand(ICommand command)
    {
        return Task.CompletedTask;
    }
}
