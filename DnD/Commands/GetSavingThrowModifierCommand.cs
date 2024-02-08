namespace DnD.Commands;

using DnD.Entities.Attributes;
using DnD.Entities.Characters;
using TableTopRpg.Commands;

internal class GetSavingThrowModifierCommand : DndCommand
{
    public GetSavingThrowModifierCommand(Character character, EAttributeType attributeType) : base(character)
    {
        AttributeType = attributeType;
    }

    public EAttributeType AttributeType { get; }

    protected override ICommandResult ExecuteDndCommand()
    {
        try
        {
            ICommand getProficiencyBonusCommand = new GetProficiencyBonusCommand(Character);
            var proficiencyBonusResult = getProficiencyBonusCommand.Execute();

            if (proficiencyBonusResult.IsSuccess && proficiencyBonusResult is IntegerResult proficiencyBonusIntegerResult)
            {
                var attribute = Character.AttributeSet.GetAttribute(AttributeType);
                var summarizedIntegerResult = SummarizedIntegerResult.Success(this, attribute.GetModifier(), attribute.Name + " Modifier");

                if (attribute.SavingThrowProficiencyLevel > 0)
                {
                    summarizedIntegerResult.BonusValues.Add((attribute.SavingThrowProficiencyLevel * proficiencyBonusIntegerResult.Value, "Proficiency Bonus"));
                }
                return summarizedIntegerResult;
            }
            else
            {
                return IntegerResult.Failure(this, proficiencyBonusResult.Message);
            }
        }
        catch (Exception e)
        {
            return IntegerResult.Failure(this, e.Message);
        }
    }
}
