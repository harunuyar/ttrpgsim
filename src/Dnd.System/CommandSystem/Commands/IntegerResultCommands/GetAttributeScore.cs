namespace Dnd.CommandSystem.Commands.IntegerResultCommands;

using Dnd.Entities.Attributes;
using Dnd.Entities.Characters;

public class GetAttributeScore : DndScoreCommand
{
    public GetAttributeScore(Character character, EAttributeType attributeType) : base(character)
    {
        AttributeType = attributeType;
    }

    public EAttributeType AttributeType { get; }

    public override void InitializeResult()
    {
        var attribute = Character.AttributeSet.GetAttribute(AttributeType);
        Result.SetBaseValue(attribute, attribute.Score);
    }
}
