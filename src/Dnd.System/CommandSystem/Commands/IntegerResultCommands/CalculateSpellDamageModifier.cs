namespace Dnd.CommandSystem.Commands.IntegerResultCommands;

using Dnd.Entities.Characters;
using Dnd.Entities.Spells;

public class CalculateSpellDamageModifier : DndScoreCommand
{
    public CalculateSpellDamageModifier(Character character, ISpell spell) : base(character)
    {
        Spell = spell;
    }

    public ISpell Spell { get; }

    public override void InitializeResult()
    {
        Result.SetBaseValue("Base", 0);
    }
}
