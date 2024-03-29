﻿namespace Dnd.Predefined.Commands.ValueCommands;

using Dnd._5eSRD.Models.Alignment;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.GameActor;

public class GetAlignment : ValueCommand<AlignmentModel>
{
    public GetAlignment(IGameActor actor) : base(actor)
    {
    }

    protected override Task InitializeResult()
    {
        if (Actor.Alignment is not null)
        {
            SetValue(Actor.Alignment.AlignmentModel, "Alignment");
        }
        
        return Task.CompletedTask;
    }
}
