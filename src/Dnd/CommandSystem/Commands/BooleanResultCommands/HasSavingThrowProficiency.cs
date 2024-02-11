namespace Dnd.CommandSystem.Commands.BooleanResultCommands;

using Dnd.Entities.Attributes;
using Dnd.Entities.Characters;

public class HasSavingThrowProficiency : DndBooleanCommand
{
    public HasSavingThrowProficiency(Character character, EAttributeType attributeType) : base(character)
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
