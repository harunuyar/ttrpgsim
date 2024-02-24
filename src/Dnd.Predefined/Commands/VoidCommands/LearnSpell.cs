namespace Dnd.Predefined.Commands.VoidCommands;

using Dnd._5eSRD.Models.Class;
using Dnd._5eSRD.Models.Spell;
using Dnd.Predefined.Commands.BoolCommands;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.GameActor;

public class LearnSpell : VoidCommand
{
    public LearnSpell(IGameActor character, ClassModel classModel, SpellModel spell) : base(character)
    {
        Class = classModel;
        Spell = spell;
    }

    public ClassModel Class { get; }

    public SpellModel Spell { get; }

    protected override async Task InitializeResult()
    {
        var canLearn = await new CanLearnSpell(Actor, Class, Spell).Execute();

        if (!canLearn.IsSuccess)
        {
            SetError("CanLearnSpell: " + canLearn.ErrorMessage);
            return;
        }

        if (!canLearn.Value)
        {
            SetError($"{Actor.Name} can't learn {Spell}. " + canLearn.Message);
            return;
        }

        if (Spell.Level is null || Spell.Level < 0 || Spell.Level > 9)
        {
            SetError("Spell level is wrong: " + Spell.Level);
            return;
        }

        if (Spell.Level == 0)
        {
            Actor.SpellMemory.LearnCantrip(Class, Spell);
            SetMessage($"{Actor.Name} has learned cantrip {Spell}.");
            return;
        }
        else
        {
            Actor.SpellMemory.PrepareSpell(Class, Spell);
        }
    }
}
