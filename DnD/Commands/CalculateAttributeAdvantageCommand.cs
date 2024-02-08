namespace DnD.Commands;

using DnD.Entities;
using DnD.Entities.Attributes;
using DnD.Entities.Characters;
using TableTopRpg.Commands;

internal class CalculateAttributeAdvantageCommand : DndCommand
{
    public CalculateAttributeAdvantageCommand(Character character, EAttributeType attributeType) : base(character)
    {
        AttributeType = attributeType;
    }

    public EAttributeType? AttributeType { get; }

    protected override ICommandResult ExecuteDndCommand()
    {
        return IntegerResult.Success(this, (int)EAdvantage.None);
    }
}
