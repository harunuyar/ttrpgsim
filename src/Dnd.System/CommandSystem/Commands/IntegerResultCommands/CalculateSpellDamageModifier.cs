namespace Dnd.System.CommandSystem.Commands.IntegerResultCommands;

using Dnd.System.Entities.Characters;
using Dnd.System.Entities.Spells;

public class CalculateSpellDamageModifier : DndScoreCommand
{
    public CalculateSpellDamageModifier(ICharacter character, ISpell spell) : base(character)
    {
        Spell = spell;
    }

    public ISpell Spell { get; }

    public override void InitializeResult()
    {
        Result.SetBaseValue("Base", 0);
    }
}
