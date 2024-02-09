namespace DnD.CommandSystem.Commands.IntegerResultCommands;

using DnD.CommandSystem.Results;
using DnD.Entities.Attributes;
using DnD.Entities.Characters;

internal class GetAttributeModifier : DndScoreCommand
{
    public GetAttributeModifier(Character character, EAttributeType attributeType) : base(character)
    {
        AttributeType = attributeType;
    }

    public EAttributeType AttributeType { get; }

    public override IntegerResultWithBonuses Execute()
    {
        var getAttributeScoreCommand = new GetAttributeScore(Character, AttributeType);
        getAttributeScoreCommand.CollectBonuses();
        var attributeScoreResult = getAttributeScoreCommand.Execute();

        if (attributeScoreResult.IsSuccess)
        {
            return IntegerResultWithBonuses.Success(this, AttributeType.ToString(), (attributeScoreResult.Value - 10) / 2, IntegerBonuses);
        }

        return IntegerResultWithBonuses.Failure(this, "Attribute score not found");
    }
}
