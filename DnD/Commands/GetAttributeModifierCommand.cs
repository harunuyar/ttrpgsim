namespace DnD.Commands;

using DnD.Entities.Attributes;
using DnD.Entities.Characters;
using TableTopRpg.Commands;

internal class GetAttributeModifierCommand : DndCommand
{
    public GetAttributeModifierCommand(Character character, EAttributeType attributeType) : base(character)
    {
        AttributeType = attributeType;
    }

    public EAttributeType AttributeType { get; }

    protected override ICommandResult ExecuteDndCommand()
    {
        try
        {
            return SummarizedIntegerResult.Success(this, Character.AttributeSet.GetAttribute(AttributeType).GetModifier(), "Base");
        }
        catch (Exception e)
        {
            return IntegerResult.Failure(this, e.Message);
        }
    }
}
