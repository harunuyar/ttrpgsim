﻿namespace Dnd.System.CommandSystem.Commands.EventCommands;

using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Effects;
using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.Events.EventListener;

public class AddEffect : DndEventCommand
{
    public AddEffect(IEventListener eventListener, IGameActor character, IEffect effect) : base(eventListener, character)
    {
        Effect = effect;
    }

    public IEffect Effect { get; }

    protected override void FinalizeResult()
    {
        Effect.StartEffect();
        Result.SetMessage($"Applied {Effect.Name} to {Effect.Target.Name}.");
    }
}
