namespace DnD.CommandSystem.Commands.IntegerResultCommands;

using DnD.Entities.Attributes;
using DnD.Entities.Characters;

internal class GetSavingThrowModifier : DndScoreCommand
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

            var getSavingThrowProficiencyLevel = new GetSavingThrowProficiencyLevel(Character, AttributeType);
            var savingThrowProficiencyLevelResult = getSavingThrowProficiencyLevel.Execute();

            if (savingThrowProficiencyLevelResult.IsSuccess && savingThrowProficiencyLevelResult.Value > 0)
            {
                var getProficiencyBonusCommand = new GetProficiencyBonus(Character);
                var proficiencyBonusResult = getProficiencyBonusCommand.Execute();

                if (proficiencyBonusResult.IsSuccess)
                {
                    Result.BonusCollection.AddBonus("Proficiency Bonus", proficiencyBonusResult.Value * savingThrowProficiencyLevelResult.Value);
                }
            }
        }
        else
        {
            Result.SetError(attributeModifierResult.ErrorMessage ?? "Couldn't get attribute modifier");
        }
    }
}
