namespace DnD.CommandSystem.Commands;

using DnD.CommandSystem.CommandResult;
using DnD.Entities.Attributes;
using DnD.Entities.Skills;
using System;

internal class CalculateAdvantageCommand : ICommand
{
    public CalculateAdvantageCommand(Skill skill)
    {
        Skill = skill;
    }

    public CalculateAdvantageCommand(EAttributeType attributeType)
    {
        AttributeType = attributeType;
    }

    public Skill? Skill { get; }

    public EAttributeType? AttributeType { get; }

    public ICommandResult Execute()
    {
        return BoolResult.Failure(this, "No advantage or disadvantage");
    }
}
