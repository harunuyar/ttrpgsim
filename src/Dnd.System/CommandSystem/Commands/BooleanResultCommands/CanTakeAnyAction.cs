namespace Dnd.System.CommandSystem.Commands.BooleanResultCommands;

using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.Entities.GameActors;

public class CanTakeAnyAction : DndBooleanCommand
{
    public CanTakeAnyAction(IGameActor character) : base(character)
    {
    }

    protected override void InitializeResult()
    {
        Result.SetValue(true, $"{Actor.Name} can take actions or reactions.");
    }
}
