namespace DnD.CommandSystem.Commands.IntegerResultCommands;

using DnD.CommandSystem.Results;
using DnD.Entities.Attributes;
using DnD.Entities.Characters;

internal class GetAttributeScore : DndScoreCommand
{
    public GetAttributeScore(Character character, EAttributeType attributeType) : base(character)
    {
        AttributeType = attributeType;
    }

    public EAttributeType AttributeType { get; }

    public override IntegerResultWithBonuses Execute()
    {
        var attribute = Character.AttributeSet.GetAttribute(AttributeType);
        return IntegerResultWithBonuses.Success(this, attribute.Name, attribute.Value, IntegerBonuses);
    }
}
