namespace Dnd.System.CommandSystem.Commands.BooleanResultCommands;

using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Spells;

public class CanDoSpellAttackAgainst : DndBooleanCommand
{
    public CanDoSpellAttackAgainst(IGameActor character, ISpell spell, IGameActor attacker) : base(character)
    {
        Spell = spell;
        Attacker = attacker;
    }

    public ISpell Spell { get; set; }

    public IGameActor Attacker { get; set; }

    protected override void InitializeResult()
    {
        SetValue(true, "By default, anyone can cast a spell on you.");
    }
}
