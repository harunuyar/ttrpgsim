namespace Dnd.System.CommandSystem.Commands.IntegerResultCommands;

using Dnd.System.Entities.Attributes;
using Dnd.System.Entities.Characters;

public class GetAttributeScore : DndScoreCommand
{
    public GetAttributeScore(ICharacter character, EAttributeType attributeType) : base(character)
    {
        AttributeType = attributeType;
    }

    public EAttributeType AttributeType { get; }

    protected override void InitializeResult()
    {
        var attribute = Character.AttributeSet.GetAttribute(AttributeType);
        Result.SetBaseValue(attribute, attribute.Score);
    }

    protected override void FinalizeResult()
    {
    }
}
