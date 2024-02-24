namespace Dnd.Predefined.Commands.BoolCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.GameActor;

public class CanTakeAnyAction : ValueCommand<bool>
{
    public CanTakeAnyAction(IGameActor character) : base(character)
    {
    }

    protected override Task InitializeResult()
    {
        SetValue(true, $"{Actor.Name} can take actions or reactions.");

        return Task.CompletedTask;
    }
}
