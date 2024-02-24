namespace Dnd.Predefined.Commands.ListCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action;
using Dnd.System.Entities.GameActor;

public class GetActions : ListCommand<IAction>
{
    public GetActions(IGameActor character) : base(character)
    {
    }
}