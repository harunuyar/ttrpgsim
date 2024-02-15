namespace Dnd.System.CommandSystem.Commands.BooleanResultCommands;

using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Spells;

public class CanCastKnownSpell : DndBooleanCommand
{
    public CanCastKnownSpell(IGameActor character, ISpell spell) : base(character)
    {
        Spell = spell;
    }

    public ISpell Spell { get; }

    protected override void InitializeResult()
    {
        SetValue(false, "By default, you can't cast a spell.");
    }
}
