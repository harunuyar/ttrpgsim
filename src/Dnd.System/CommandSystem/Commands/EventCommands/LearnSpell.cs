namespace Dnd.System.CommandSystem.Commands.EventCommands;

using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Spells;

public class LearnSpell : DndEventCommand
{
    public LearnSpell(IEventListener eventListener, IGameActor character, ISpell spell) : base(eventListener, character)
    {
        Spell = spell;
    }

    public ISpell Spell { get; }

    protected override void FinalizeResult()
    {
        if (Result.Message == null)
        {
            Result.SetError("Failed to add spell to known spells.");
        }
    }
}
