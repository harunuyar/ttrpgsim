namespace Dnd.System.CommandSystem.Commands.IntegerResultCommands;

using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.Entities.Attributes;
using Dnd.System.Entities.GameActors;

public class GetTotalAttributeScore : DndScoreCommand
{
    public GetTotalAttributeScore(IGameActor character, EAttributeType attributeType) : base(character)
    {
        AttributeType = attributeType;
    }

    public EAttributeType AttributeType { get; }

    protected override void InitializeResult()
    {
        var getBaseAttributeScore = new GetBaseAttributeScore(Actor, AttributeType).Execute();

        if (!getBaseAttributeScore.IsSuccess)
        {
            SetErrorAndReturn("GetBaseAttributeScore: " + getBaseAttributeScore.ErrorMessage);
            return;
        }

        Result.Set(getBaseAttributeScore);
    }
}
