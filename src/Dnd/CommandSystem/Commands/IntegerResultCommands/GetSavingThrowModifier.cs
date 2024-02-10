namespace Dnd.CommandSystem.Commands.IntegerResultCommands;

using Dnd.CommandSystem.Commands.BooleanResultCommands;
using Dnd.Entities.Attributes;
using Dnd.Entities.Characters;

public class GetSavingThrowModifier : DndScoreCommand
{
    public GetSavingThrowModifier(Character character, EAttributeType attributeType) : base(character)
    {
        AttributeType = attributeType;
    }

    public EAttributeType AttributeType { get; }

    public override void InitializeResult()
    {
        var getAttributeModifierCommand = new GetAttributeModifier(Character, AttributeType);
        var attributeModifierResult = getAttributeModifierCommand.Execute();

        if (attributeModifierResult.IsSuccess)
        {
            Result.SetBaseValue(Character.AttributeSet.GetAttribute(AttributeType), attributeModifierResult.Value);

            var getSavingThrowProficiencyLevel = new GetSavingThrowProficiency(Character, AttributeType);
            var savingThrowProficiencyLevelResult = getSavingThrowProficiencyLevel.Execute();

            if (savingThrowProficiencyLevelResult.IsSuccess && savingThrowProficiencyLevelResult.Value)
            {
                var getProficiencyBonusCommand = new GetProficiencyBonus(Character);
                var proficiencyBonusResult = getProficiencyBonusCommand.Execute();

                if (proficiencyBonusResult.IsSuccess)
                {
                    Result.BonusCollection.AddBonus("Proficiency Bonus", proficiencyBonusResult.Value);
                }
            }
        }
        else
        {
            Result.SetError(attributeModifierResult.ErrorMessage ?? "Couldn't get attribute modifier");
        }
    }
}
