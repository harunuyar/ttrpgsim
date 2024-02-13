namespace Dnd.System.CommandSystem.Commands.ListCommands;

using Dnd.System.Entities.Actions;
using Dnd.System.Entities.GameActors;

internal class GetActions : DndListCommand<IAction>
{
    public GetActions(IGameActor character) : base(character)
    {
    }
}