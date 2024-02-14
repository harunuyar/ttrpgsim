namespace Dnd.System.CommandSystem.Commands.ListCommands;

using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Spells;

public class GetKnownSpells : DndListCommand<ISpell>
{
    public GetKnownSpells(IGameActor character) : base(character)
    {
    }
}
