﻿namespace Dnd.System.Entities.Action.ActionTypes;

using Dnd.System.Entities.GameActor;

public interface IAction : ICommandHandler, IUsageBonusProvider
{
    IGameActor ActionOwner { get; }
    string Name { get; }
    ActionDurationType ActionDuration { get; }
    EReactionType ReactionType { get; }
    Task<bool> IsAvailable();
}
