namespace Dnd.Predefined.Commands.ValueCommands;

using Dnd._5eSRD.Models.Common;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.GameActor;

public class GetCreatureSize : ValueCommand<ECreatureSize>
{
    public GetCreatureSize(IGameActor character) : base(character)
    {
    }

    protected override Task InitializeResult()
    {
        SetValue(Actor.Race.RaceModel.Size ?? ECreatureSize.None, $"{Actor.Name} is {Actor.Race.RaceModel.Size}.");

        return Task.CompletedTask;
    }
}
