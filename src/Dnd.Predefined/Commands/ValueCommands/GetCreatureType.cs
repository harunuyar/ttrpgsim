namespace Dnd.Predefined.Commands.ValueCommands;

using Dnd._5eSRD.Models.Common;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.GameActor;

public class GetCreatureType : ValueCommand<CreatureType>
{
    public GetCreatureType(IGameActor character) : base(character)
    {
    }

    protected override Task InitializeResult()
    {
        SetValue(CreatureType.Humanoid, $"{Actor.Name} is a humanoid.");

        return Task.CompletedTask;
    }
}
