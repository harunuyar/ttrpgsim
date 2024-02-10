namespace DnD.CommandSystem.Commands.BooleanResultCommands;

using DnD.Entities.Attributes;
using DnD.Entities.Characters;

internal class GetSavingThrowProficiency : DndBooleanCommand
{
    public GetSavingThrowProficiency(Character character, EAttributeType attributeType) : base(character)
    {
        AttributeType = attributeType;
    }

    public EAttributeType AttributeType { get; }

    public override void InitializeResult()
    {
        var attribute = Character.AttributeSet.GetAttribute(AttributeType);
        Result.SetValue("Base", attribute.SavingThrowProficiency);
    }
}
