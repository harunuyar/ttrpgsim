namespace Dnd.Predefined.Feats.Proficiency;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.CommandSystem.Commands.BooleanResultCommands;
using Dnd.System.CommandSystem.Commands.IntegerResultCommands;
using Dnd.System.Entities.Attributes;

public class SavingThrowProficiency : AFeat
{
    public SavingThrowProficiency(EAttributeType attributeTypes, EProficiencyType proficiencyType = EProficiencyType.FullProficiency) : base("Saving Throw Proficiency", string.Empty)
    {
        AttributeTypes = attributeTypes;
        ProficiencyType = proficiencyType;
    }

    public EAttributeType AttributeTypes { get; }

    public EProficiencyType ProficiencyType { get; private set; }

    public override string Description => GetDescription();

    public override void HandleCommand(ICommand command)
    {
        if (command is HasSavingThrowProficiency hasSavingThrowProficiency && AttributeTypes.HasFlag(hasSavingThrowProficiency.AttributeType))
        {
            hasSavingThrowProficiency.SetValue(this, true);
        }
        else if (command is GetSavingThrowModifier getSavingThrowModifier && AttributeTypes.HasFlag(getSavingThrowModifier.AttributeType))
        {
            var getProficiencyBonus = new GetProficiencyBonus(getSavingThrowModifier.Actor);
            var proficiencyBonusResult = getProficiencyBonus.Execute();

            if (proficiencyBonusResult.IsSuccess)
            {
                getSavingThrowModifier.AddBonus(this, ProficiencyType.GetProficiencyModifier(proficiencyBonusResult.Value));
            }
            else
            {
                getSavingThrowModifier.SetErrorAndReturn("Couldn't get proficiency bonus: " + proficiencyBonusResult.ErrorMessage);
            }
        }
    }

    private string GetDescription()
    {
        var list = new[] { EAttributeType.Strength, EAttributeType.Dexterity, EAttributeType.Constitution, EAttributeType.Intelligence, EAttributeType.Wisdom, EAttributeType.Charisma }
            .Where(at => AttributeTypes.HasFlag(at))
            .Select(at => at.ToString())
            .ToList();

        return $"You have {ProficiencyType} in saving throws that use your {string.Join(", ", list)} modifier(s).";
    }
}
