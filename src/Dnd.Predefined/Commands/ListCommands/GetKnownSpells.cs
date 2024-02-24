namespace Dnd.Predefined.Commands.ListCommands;

using Dnd._5eSRD.Models.Spell;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.GameActor;

public class GetKnownSpells : ListCommand<SpellModel>
{
    public GetKnownSpells(IGameActor character) : base(character)
    {
    }

    protected override Task InitializeResult()
    {
        AddValues(Actor.SpellMemory.GetCantrips(), "Known Cantrips");
        AddValues(Actor.SpellMemory.GetPreparedSpells(), "Prepared Spells");

        return Task.CompletedTask;
    }
}
