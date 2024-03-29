﻿namespace Dnd.System.Entities.Action;

using Dnd.System.Entities.Events;
using Dnd.System.Entities.GameActor;

public interface IEventReaction : IAction
{
    bool MandatoryReaction { get; }
    Task<bool> IsReactionAvailable(IGameActor gameActor, IEvent eventToReactTo);
    Task<IEvent> CreateReactionEvent(IGameActor gameActor, IEvent eventToReactTo);
}
