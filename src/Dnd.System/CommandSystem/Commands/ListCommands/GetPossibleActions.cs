namespace Dnd.System.CommandSystem.Commands.ListCommands;

using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.Entities.Actions;
using Dnd.System.Entities.GameActors;

public class GetPossibleActions : DndListCommand<IAction>
{
    public GetPossibleActions(IGameActor character) : base(character)
    {
    }
}