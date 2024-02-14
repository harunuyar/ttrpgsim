namespace Dnd.System.CommandSystem.Commands.IntegerResultCommands;

using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Spells;

public class CalculateSpellDamageModifier : DndScoreCommand
{
    public CalculateSpellDamageModifier(IGameActor character, ISpell spell) : base(character)
    {
        Spell = spell;
    }

    public ISpell Spell { get; }

    protected override void InitializeResult()
    {
        Result.SetBaseValue("Base", 0);
    }
}
