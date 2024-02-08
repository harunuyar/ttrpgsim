namespace DnD.Commands;

using DnD.Entities.Attributes;
using DnD.Entities.Characters;
using DnD.Entities.Classes;
using DnD.GameManagers.Dice;
using TableTopRpg.Commands;

internal class CalculateHitPointsCommand : DndCommand
{
    public CalculateHitPointsCommand(Character character, IDndClass dndClass) : base(character)
    {
        DndClass = dndClass;
    }

    public IDndClass DndClass { get; }

    protected override ICommandResult ExecuteDndCommand()
    {
        ICommand constitutionModifierCommand = new GetAttributeModifierCommand(Character, EAttributeType.Constitution);
        ICommandResult modifierResult = constitutionModifierCommand.Execute();

        if (modifierResult.IsSuccess && modifierResult is IntegerResult valueResult)
        {
            SummarizedIntegerResult summarizedResult;

            if (Character.Level == 1)
            {
                summarizedResult = SummarizedIntegerResult.Success(this, (int)DndClass.HitDie, "Max Hit Die");
            }
            else
            {
                summarizedResult = SummarizedIntegerResult.Success(this, DiceManager.RollDice(DndClass.HitDie), "Hit Die");
            }

            summarizedResult.BonusValues.Add((valueResult.Value, "Constitution Modifier"));
            return summarizedResult;
        }
        else
        {
            return IntegerResult.Failure(this, "Failed to calculate hit points");
        }
    }
}