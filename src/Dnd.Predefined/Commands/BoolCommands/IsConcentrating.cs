namespace Dnd.Predefined.Commands.BoolCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.GameActor;

public class IsConcentrating : ValueCommand<bool>
{
    public IsConcentrating(IGameActor actor) : base(actor)
    {
    }

    protected override Task InitializeResult()
    {
        SetValue(false, $"{Actor.Name} is not concentrating on any spell.");

        return Task.CompletedTask;
    }
}
