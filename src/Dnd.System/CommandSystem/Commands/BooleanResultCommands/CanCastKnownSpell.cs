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
        SetValue(false, $"{Actor.Name} can't cast spell {Spell.Name}.");
    }
}
