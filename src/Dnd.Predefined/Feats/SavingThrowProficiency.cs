namespace Dnd.Predefined.Feats;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.CommandSystem.Commands.BooleanResultCommands;
using Dnd.System.Entities.Attributes;

public class SavingThrowProficiency : AFeat
{
    public SavingThrowProficiency(EAttributeType attributeTypes) : base("Saving Throw Proficiency", GetDescription(attributeTypes))
    {
        AttributeTypes = attributeTypes;
    }

    public EAttributeType AttributeTypes { get; }

    public override void HandleCommand(DndCommand command)
    {
        if (command is HasSavingThrowProficiency hasSavingThrowProficiency && AttributeTypes.HasFlag(hasSavingThrowProficiency.AttributeType))
        {
            hasSavingThrowProficiency.Result.SetValue(this, true);
        }
    }

    private static string GetDescription(EAttributeType attributeTypes)
    {
        var list = new[] { EAttributeType.Strength, EAttributeType.Dexterity, EAttributeType.Constitution, EAttributeType.Intelligence, EAttributeType.Wisdom, EAttributeType.Charisma }
            .Where(at => attributeTypes.HasFlag(at))
            .Select(at => at.ToString())
            .ToList();

        return $"You are proficient in saving throws that use your {string.Join(", ", list)} modifier.";
    }
}
