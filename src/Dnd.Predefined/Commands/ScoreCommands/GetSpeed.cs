namespace Dnd.Predefined.Commands.ScoreCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.GameActor;

public class GetSpeed : ScoreCommand
{
    public GetSpeed(IGameActor character) : base(character)
    {
    }

    protected override Task InitializeResult()
    {
        if (!Actor.Race.RaceModel.Speed.HasValue)
        {
            SetError("Actor.Race.RaceModel.Speed is null");
            return Task.CompletedTask;
        }

        SetBaseValue(Actor.Race.RaceModel.Speed.Value, "Base");

        return Task.CompletedTask;
    }
}
