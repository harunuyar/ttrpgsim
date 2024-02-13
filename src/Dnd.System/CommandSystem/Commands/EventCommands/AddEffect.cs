﻿namespace Dnd.System.CommandSystem.Commands.EventCommands;

using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Effects;

public class AddEffect : DndEventCommand
{
    public AddEffect(IEventListener eventListener, IGameActor character, IEffect effect) : base(eventListener, character)
    {
        Effect = effect;
    }

    public IEffect Effect { get; }

    protected override void InitializeEvent()
    {
    }

    protected override void FinalizeEvent()
    {
        Effect.StartEffect();
        EventResult.SetMessage($"Applied {Effect.Name} to {Effect.Target.Name}");
    }
}