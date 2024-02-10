namespace DnD.CommandSystem.Commands.IntegerResultCommands;

using DnD.Entities.Attributes;
using DnD.Entities.Characters;

internal class GetSavingThrowProficiencyLevel : DndScoreCommand
{
    public GetSavingThrowProficiencyLevel(Character character, EAttributeType attributeType) : base(character)
    {
        AttributeType = attributeType;
    }

    public EAttributeType AttributeType { get; }

    public override void InitializeResult()
    {
        var attribute = Character.AttributeSet.GetAttribute(AttributeType);
        Result.SetBaseValue(attribute, attribute.SavingThrowProficiencyLevel);
    }
}
