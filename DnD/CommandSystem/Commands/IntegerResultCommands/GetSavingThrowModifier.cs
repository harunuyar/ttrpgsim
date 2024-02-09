namespace DnD.CommandSystem.Commands.IntegerResultCommands;

using DnD.CommandSystem.Results;
using DnD.Entities.Attributes;
using DnD.Entities.Characters;

internal class GetSavingThrowModifier : DndScoreCommand
{
    public GetSavingThrowModifier(Character character, EAttributeType attributeType) : base(character)
    {
        AttributeType = attributeType;
    }

    public EAttributeType AttributeType { get; }

    override public void CollectBonuses()
    {
        var getAttributeModifierCommand = new GetAttributeModifier(Character, AttributeType);
        getAttributeModifierCommand.CollectBonuses();
        var attributeModifierResult = getAttributeModifierCommand.Execute();

        if (attributeModifierResult.IsSuccess)
        {
            IntegerBonuses.AddBonus(AttributeType.ToString(), attributeModifierResult.Value);
        }

        var getProficiencyBonusCommand = new GetProficiencyBonus(Character);
        getAttributeModifierCommand.CollectBonuses();
        var proficiencyBonusResult = getProficiencyBonusCommand.Execute();

        if (proficiencyBonusResult.IsSuccess && proficiencyBonusResult.Value > 0)
        {
            var getSavingThrowProficiencyLevel = new GetSavingThrowProficiencyLevel(Character, AttributeType);
            getSavingThrowProficiencyLevel.CollectBonuses();
            var savingThrowProficiencyLevelResult = getSavingThrowProficiencyLevel.Execute();

            if (savingThrowProficiencyLevelResult.IsSuccess && savingThrowProficiencyLevelResult.Value > 0)
            {
                IntegerBonuses.AddBonus("Proficiency Bonus", proficiencyBonusResult.Value * savingThrowProficiencyLevelResult.Value);
            }
        }

        base.CollectBonuses();
    }

    public override IntegerBonuses Execute()
    {
        return IntegerBonuses;
    }
}
