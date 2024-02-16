namespace Dnd.System.CommandSystem.Commands.IntegerResultCommands;

using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.Entities.GameActors;

public class GetSpeed : DndScoreCommand
{
    public GetSpeed(IGameActor character) : base(character)
    {

    }

    protected override void InitializeResult()
    {
        Result.SetBaseValue(Actor.Race, (int)Actor.Race.Speed.Feet);
    }
}
