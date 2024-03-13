namespace Dnd.Predefined.Commands.ListCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Events;
using Dnd.System.Entities.GameActor;

public class GetReactionEvents : ListCommand<IEvent>
{
    public GetReactionEvents(IGameActor actor, IEvent @event) : base(actor)
    {
        Event = @event;
    }

    public IEvent Event { get; }
}
