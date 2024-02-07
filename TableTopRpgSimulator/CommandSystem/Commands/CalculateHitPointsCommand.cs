namespace DnD.CommandSystem.Commands;

using DnD.CommandSystem.CommandResult;
using DnD.Entities.Attributes;
using DnD.Entities.Characters;
using DnD.Entities.Classes;
using DnD.GameManagers.Dice;

internal class CalculateHitPointsCommand : ICommand
{
    public CalculateHitPointsCommand(Player player, Class dndClass)
    {
        Player = player;
        DndClass = dndClass;
    }

    public Player Player { get; }
    public Class DndClass { get; }

    public ICommandResult Execute()
    {
        ICommand constitutionModifierCommand = new GetAttributeModifierCommand(Player, EAttributeType.Constitution);
        ICommandResult modifierResult = constitutionModifierCommand.Execute();

        if (modifierResult.IsSuccess && modifierResult is IntegerResult valueResult)
        {
            if (Player.Level == 1)
            {
                return IntegerResult.Success(this, (int)DndClass.HitDie + valueResult.Value);
            }

            return IntegerResult.Success(this, DiceManager.RollDice(DndClass.HitDie) + valueResult.Value);
        }
        else
        {
            return IntegerResult.Failure(this, "Failed to calculate hit points");
        }
    }
}