namespace Dnd.System.CommandSystem.Commands.ListCommands;

using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.Entities.Actions.BaseActions;
using Dnd.System.Entities.GameActors;

public class GetActions : DndListCommand<IAction>
{
    public GetActions(IGameActor character) : base(character)
    {
    }
}