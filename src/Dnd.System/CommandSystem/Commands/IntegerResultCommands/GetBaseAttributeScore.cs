namespace Dnd.System.CommandSystem.Commands.IntegerResultCommands;

using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.Entities.Attributes;
using Dnd.System.Entities.GameActors;

public class GetBaseAttributeScore : DndScoreCommand
{
    public GetBaseAttributeScore(IGameActor character, EAttributeType attributeType) : base(character)
    {
        AttributeType = attributeType;
    }

    public EAttributeType AttributeType { get; }

    protected override void InitializeResult()
    {
        var attribute = Actor.AttributeSet.GetAttribute(AttributeType);
        Result.SetBaseValue(attribute, attribute.Score);
    }
}
