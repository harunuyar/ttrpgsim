namespace Dnd.System.CommandSystem.Commands.ListCommands;

using Dnd.System.Entities.Actions;
using Dnd.System.Entities.GameActors;

internal class GetPossibleActions : DndListCommand<IAction>
{
    public GetPossibleActions(IGameActor character) : base(character)
    {
    }
}