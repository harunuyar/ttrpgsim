﻿namespace Dnd.System.CommandSystem.Commands.BooleanResultCommands;

using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.Entities.Attributes;
using Dnd.System.Entities.GameActors;

public class HasSavingThrowProficiency : DndBooleanCommand
{
    public HasSavingThrowProficiency(IGameActor character, EAttributeType attributeType) : base(character)
    {
        AttributeType = attributeType;
    }

    public EAttributeType AttributeType { get; }

    protected override void InitializeResult()
    {
        Result.SetValue(false, $"{Actor.Name} doesn't have {AttributeType} saving throw proficiency.");
    }
}
