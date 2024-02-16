﻿namespace Dnd.System.CommandSystem.Commands.EventCommands;

using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Effects;
using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.Events.EventListener;

internal class RemoveEffect : DndEventCommand
{
    public RemoveEffect(IEventListener eventListener, IGameActor character, IEffect effect) : base(eventListener, character)
    {
        Effect = effect;
    }

    public IEffect Effect { get; }

    protected override void FinalizeResult()
    {
        Effect.RemoveEffect();
        Result.SetMessage($"Removed {Effect.Name} from {Effect.Target.Name}.");
    }
}
