﻿namespace Dnd.System.CommandSystem.Commands.IntegerResultCommands;

using Dnd.System.Entities.Attributes;
using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Items.Equipments.Weapons;

public class GetSavingThrowModifier : DndScoreCommand
{
    public GetSavingThrowModifier(IGameActor character, EAttributeType attributeType, EDamageType damageType) : base(character)
    {
        AttributeType = attributeType;
        DamageType = damageType;
    }

    public EAttributeType AttributeType { get; }

    public EDamageType DamageType { get; }

    protected override void InitializeResult()
    {
        var getAttributeModifierCommand = new GetAttributeModifier(Character, AttributeType);
        var attributeModifierResult = getAttributeModifierCommand.Execute();

        if (attributeModifierResult.IsSuccess)
        {
            Result.SetBaseValue(Character.AttributeSet.GetAttribute(AttributeType), attributeModifierResult.Value);
        }
        else
        {
            Result.SetError(attributeModifierResult.ErrorMessage ?? "Couldn't get attribute modifier");
        }
    }

    protected override void FinalizeResult()
    {
    }
}
