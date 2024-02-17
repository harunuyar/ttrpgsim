namespace Dnd.System.CommandSystem.Commands.IntegerResultCommands.Modifiers;

using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Spells;

public class GetSpellAttackModifierAgainst : GetAttackModifierAgainst
{
    internal GetSpellAttackModifierAgainst(IGameActor character, ISpell spell, IGameActor attacker) : base(character, attacker)
    {
        Spell = spell;
    }

    public ISpell Spell { get; }
}