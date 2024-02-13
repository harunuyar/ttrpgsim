namespace Dnd.System.CommandSystem.Commands.ListCommands;

using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Spells;

internal class GetSpells : DndListCommand<ISpell>
{
    public GetSpells(IGameActor character) : base(character)
    {
    }
}
