namespace Dnd.System.CommandSystem.Commands.ValueCommands;

using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Races;

public class GetCreatureSize : DndValueCommand<ISize>
{
    public GetCreatureSize(IGameActor character) : base(character)
    {
    }

    protected override void InitializeResult()
    {
        Result.Value = Actor.Race.Size;
    }
}
