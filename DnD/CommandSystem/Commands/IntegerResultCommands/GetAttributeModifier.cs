namespace DnD.CommandSystem.Commands.IntegerResultCommands;

using DnD.Entities.Attributes;
using DnD.Entities.Characters;

internal class GetAttributeModifier : DndScoreCommand
{
    public GetAttributeModifier(Character character, EAttributeType attributeType) : base(character)
    {
        AttributeType = attributeType;
    }

    public EAttributeType AttributeType { get; }

    public override void InitializeResult()
    {
        var getAttributeScoreCommand = new GetAttributeScore(Character, AttributeType);
        var attributeScoreResult = getAttributeScoreCommand.Execute();

        if (attributeScoreResult.IsSuccess)
        {
            Result.SetBaseValue(Character.AttributeSet.GetAttribute(AttributeType), (attributeScoreResult.Value - 10) / 2);
        }
        else
        {
            Result.SetError(attributeScoreResult.ErrorMessage ?? "Unknown");
        }
    }
}
