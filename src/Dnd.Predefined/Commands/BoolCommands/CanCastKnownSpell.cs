namespace Dnd.Predefined.Commands.BoolCommands;

using Dnd._5eSRD.Models.Spell;
using Dnd.Predefined.Commands.ScoreCommands;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.GameActor;

public class CanCastKnownSpell : ValueCommand<bool>
{
    public CanCastKnownSpell(IGameActor character, SpellModel spell) : base(character)
    {
        Spell = spell;
    }

    public SpellModel Spell { get; }

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

        if (Spell.Level == null || Spell.Level < 0 || Spell.Level > 9)
        {
            SetError("Spell level is wrong: " + Spell.Level);
            return;
        }

        if (Spell.Level == 0)
        {
            if (Actor.SpellMemory.HasCantrip(Spell))
            {
                SetValue(true, $"{Actor.Name} can cast cantrip {Spell}.");
            }
            else
            {
                SetValue(false, $"{Actor.Name} doesn't know cantrip {Spell}.");
            }
        }
        else
        {
            if (Actor.SpellMemory.HasPreparedSpell(Spell))
            {
                var spellSlot = await new GetAvailableSpellSlots(Actor, Spell.Level.Value).Execute();

                if (!spellSlot.IsSuccess)
                {
                    SetError("GetAvailableSpellSlots: " + spellSlot.ErrorMessage);
                    return;
                }

                if (spellSlot.Value > 0)
                {
                    SetValue(true, $"{Actor.Name} can cast spell {Spell}.");
                }
                else
                {
                    SetValue(false, $"{Actor.Name} doesn't have spell slot for spell {Spell}.");
                }
            }
            else
            {
                SetValue(false, $"{Actor.Name} doesn't know spell {Spell}.");
            }
        }
    }
}
