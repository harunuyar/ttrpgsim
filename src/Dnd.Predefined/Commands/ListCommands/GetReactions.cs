namespace Dnd.Predefined.Commands.ListCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.Events;
using Dnd.System.Entities.GameActor;

public class GetReactions : ListCommand<IReaction>
{
    public GetReactions(IGameActor actor, IEvent @event) : base(actor)
    {
        Event = @event;
    }

    public IEvent Event { get; }
}
