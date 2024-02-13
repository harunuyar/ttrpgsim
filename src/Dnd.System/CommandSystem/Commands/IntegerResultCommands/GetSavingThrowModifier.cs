namespace Dnd.System.CommandSystem.Commands.IntegerResultCommands;

using Dnd.System.Entities.Attributes;
using Dnd.System.Entities.Characters;
using Dnd.System.Entities.Items.Equipments.Weapons;

public class GetSavingThrowModifier : DndScoreCommand
{
    public GetSavingThrowModifier(ICharacter character, EDamageType damageType, EAttributeType attributeType) : base(character)
    {
        DamageType = damageType;
        AttributeType = attributeType;
    }

    public EDamageType DamageType { get; }

    public EAttributeType AttributeType { get; }

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
