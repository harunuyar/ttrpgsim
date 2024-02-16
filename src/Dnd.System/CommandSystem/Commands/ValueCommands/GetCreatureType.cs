namespace Dnd.System.CommandSystem.Commands.ValueCommands;

using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Races;

public class GetCreatureType : DndValueCommand<ICreatureType>
{
    public GetCreatureType(IGameActor character) : base(character)
    {
    }

    protected override void InitializeResult()
    {
        Result.Value = Actor.Race.CreatureType;
    }
}
