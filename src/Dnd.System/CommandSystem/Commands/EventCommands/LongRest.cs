﻿namespace Dnd.System.CommandSystem.Commands.EventCommands;

using Dnd.System.Entities.GameActors;

public class LongRest : DndEventCommand
{
    public LongRest(IEventListener eventListener, IGameActor character, TimeSpan? timeSpent) : base(eventListener, character)
    {
        TimeSpent = timeSpent ?? TimeSpan.FromHours(8);
    }

    public TimeSpan TimeSpent { get; }

    protected override void InitializeResult()
    {
        var passTime = new PassTime(EventListener, Actor, TimeSpent);
        var passTimeResult = passTime.Execute();

        if (!passTimeResult.IsSuccess)
        {
            SetErrorAndReturn("PassTime: " + passTimeResult.ErrorMessage);
        }
    }

    protected override void FinalizeResult()
    {
        Result.SetMessage($"You have rested for {TimeSpent.TotalHours} hours.");
    }
}