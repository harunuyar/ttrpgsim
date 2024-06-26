﻿namespace Dnd.Predefined.Commands.VoidCommands;

using Dnd.Predefined.Commands.ScoreCommands;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.GameActor;

public class ApplyHeal : VoidCommand
{
    public ApplyHeal(IGameActor character, int amount) : base(character)
    {
        Amount = amount;
    }

    public int Amount { get; }

    protected override Task InitializeResult()
    {
        return new GetMaxHP(Actor).Execute(); // this will update max hp
    }

    protected override Task FinalizeResult()
    {
        Actor.HitPoints.Heal(Amount);
        SetMessage($"Healed {Actor.Name} by {Amount}.");

        return Task.CompletedTask;
    }
}
