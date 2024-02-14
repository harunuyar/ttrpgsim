namespace Dnd.System.CommandSystem.Commands.IntegerResultCommands;

using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Spells;

public class CalculateSpellAttackModifierAgainst : DndScoreCommand
{
    public CalculateSpellAttackModifierAgainst(IGameActor character, ISpell spell, IGameActor attacker) : base(character)
    {
        Spell = spell;
        Attacker = attacker;
    }

    public ISpell Spell { get; }

    public IGameActor Attacker { get; }

    protected override void InitializeResult()
    {
        Result.SetBaseValue("Base", 0);
    }
}