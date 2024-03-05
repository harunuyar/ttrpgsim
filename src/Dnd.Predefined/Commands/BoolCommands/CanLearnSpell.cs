namespace Dnd.Predefined.Commands.BoolCommands;

using Dnd._5eSRD.Models.Spell;
using Dnd.Predefined.Commands.ScoreCommands;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.GameActor;
using Dnd.System.Entities.Instances;

public class CanLearnSpell : ValueCommand<bool>
{
    public CanLearnSpell(IGameActor character, ISpellcastingAbility spellcastingAbility, SpellModel spell) : base(character)
    {
        Spell = spell;
        SpellcastingAbility = spellcastingAbility;
    }

    public SpellModel Spell { get; }

    public ISpellcastingAbility SpellcastingAbility { get; }

    protected override async Task InitializeResult()
    {
        var canTakeAnyAction = await new CanTakeAnyAction(Actor).Execute();

        if (!canTakeAnyAction.IsSuccess)
        {
            SetError("CanTakeAnyAction: " + canTakeAnyAction.ErrorMessage);
            return;
        }

        if (!canTakeAnyAction.Value)
        {
            Set(canTakeAnyAction);
            ForceComplete();
            return;
        }

        if (Spell.Level is null || Spell.Level < 0 || Spell.Level > 9)
        {
            SetError("Spell level is wrong: " + Spell.Level);
            return;
        }

        if (Spell.Level == 0)
        {
            var maxCantrips = await new GetMaxKnownCantripsCount(Actor, SpellcastingAbility).Execute();

            if (!maxCantrips.IsSuccess)
            {
                SetError("GetMaxKnownCantripsCount: " + maxCantrips.ErrorMessage);
                return;
            }

            if (SpellcastingAbility.GetCantripActions().Count >= maxCantrips.Value)
            {
                SetValue(false, $"{Actor.Name} can't learn cantrip {Spell.Name} because they already know the maximum number of cantrips.");
                return;
            }

            SetValue(true, $"{Actor.Name} can learn cantrip {Spell.Name}.");
            return;
        }
        else
        {
            var maxSpells = await new GetMaxKnownSpellsCount(Actor, SpellcastingAbility).Execute();

            if (!maxSpells.IsSuccess)
            {
                SetError("GetMaxKnownSpellsCount: " + maxSpells.ErrorMessage);
                return;
            }

            if (SpellcastingAbility.GetSpellActions().Count >= maxSpells.Value)
            {
                SetValue(false, $"{Actor.Name} can't learn {Spell.Name} because they already know the maximum number of spells.");
                return;
            }

            var maxSpellSlots = await new GetMaxSpellSlotsCount(Actor, Spell.Level.Value).Execute();

            if (!maxSpellSlots.IsSuccess)
            {
                SetError("GetMaxSpellSlotsCount: " + maxSpellSlots.ErrorMessage);
                return;
            }

            if (maxSpellSlots.Value == 0)
            {
                SetValue(false, $"{Actor.Name} can't learn {Spell.Name} because they don't have any spell slots for that level.");
                return;
            }

            SetValue(true, $"{Actor.Name} can learn {Spell.Name}.");
            return;
        }
    }
}
