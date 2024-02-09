namespace DnD.CommandSystem.Commands.IntegerResultCommands;

using DnD.CommandSystem.Results;
using DnD.Entities.Attributes;
using DnD.Entities.Characters;

internal class GetSavingThrowProficiencyLevel : DndScoreCommand
{
    public GetSavingThrowProficiencyLevel(Character character, EAttributeType attributeType) : base(character)
    {
        AttributeType = attributeType;
    }

    public EAttributeType AttributeType { get; }

    public override IntegerResultWithBonuses Execute()
    {
        return IntegerResultWithBonuses.Success(this, "Base", Character.AttributeSet.GetAttribute(AttributeType).SavingThrowProficiencyLevel, IntegerBonuses);
    }
}
