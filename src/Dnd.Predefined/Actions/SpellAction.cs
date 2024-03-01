namespace Dnd.Predefined.Actions;

using Dnd._5eSRD.Models.Spell;
using Dnd.Predefined.Commands.BoolCommands;
using Dnd.System.Entities.Action;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.GameActor;

public class SpellAction : Action, ISpellAction
{
    public SpellAction(IGameActor actionOwner, SpellModel spellModel, int spellSlot) 
        : base(actionOwner, spellModel.Name ?? "Unknown", ActionDurationType.FromString(spellModel.CastingTime) ?? ActionDurationType.Action)
    {
        Spell = spellModel;
        SpellSlot = spellSlot;
    }

    public SpellModel Spell { get; }

    public int SpellSlot { get; }

    public override async Task<bool> IsAvailable()
    {
        if (await base.IsAvailable())
        {
            var canCast = await new CanCastKnownSpell(ActionOwner, Spell, SpellSlot).Execute();

            if (!canCast.IsSuccess)
            {
                throw new InvalidOperationException("CanCastKnownSpell: " + canCast.ErrorMessage);
            }

            return canCast.Value;
        }

        return false;
    }
}
