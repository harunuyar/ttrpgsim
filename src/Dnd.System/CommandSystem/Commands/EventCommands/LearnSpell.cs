namespace Dnd.System.CommandSystem.Commands.EventCommands;

using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Spells;
using Dnd.System.Events.EventListener;

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
            Result.SetError($"{Actor.Name} has failed to add {Spell.Name} to known spells.");
        }
    }
}
