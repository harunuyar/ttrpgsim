namespace Dnd.System.CommandSystem.Commands.BooleanResultCommands;

using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Spells;

public class CanLearnSpell : DndBooleanCommand
{
    public CanLearnSpell(IGameActor character, ISpell spell) : base(character)
    {
        Spell = spell;
    }

    public ISpell Spell { get; }

    protected override void InitializeResult()
    {
        Result.SetValue(false, $"{Actor.Name} can't learn {Spell.Name}.");
    }
}
