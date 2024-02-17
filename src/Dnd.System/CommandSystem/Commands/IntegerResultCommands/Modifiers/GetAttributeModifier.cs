namespace Dnd.System.CommandSystem.Commands.IntegerResultCommands.Modifiers;

using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.Entities.Attributes;
using Dnd.System.Entities.GameActors;

public class GetAttributeModifier : DndScoreCommand
{
    public GetAttributeModifier(IGameActor character, EAttributeType attributeType) : base(character)
    {
        AttributeType = attributeType;
    }

    public EAttributeType AttributeType { get; }

    protected override void InitializeResult()
    {
        var getAttributeScoreCommand = new GetAttributeScore(Actor, AttributeType);
        var attributeScoreResult = getAttributeScoreCommand.Execute();

        if (!attributeScoreResult.IsSuccess)
        {
            SetErrorAndReturn("GetAttributeScore: " + attributeScoreResult.ErrorMessage);
            return;
        }

        Result.SetBaseValue(Actor.AttributeSet.GetAttribute(AttributeType), (attributeScoreResult.Value - 10) / 2);
    }
}
