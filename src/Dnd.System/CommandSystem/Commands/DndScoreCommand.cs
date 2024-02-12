namespace Dnd.System.CommandSystem.Commands;

using Dnd.System.CommandSystem.Results;
using Dnd.System.Entities.Characters;

public abstract class DndScoreCommand : ICommand
{
    public DndScoreCommand(ICharacter character)
    {
        Character = character;
        Result = IntegerResultWithBonus.Empty(this);
        ShouldCollectBonuses = true;
    }

    public ICharacter Character { get; }

    public IntegerResultWithBonus Result { get; }

    protected bool ShouldCollectBonuses { get; set; }

    public IntegerResultWithBonus Execute()
    {
        Result.Reset();

        InitializeResult();

        if (ShouldCollectBonuses && Result.IsSuccess)
        {
            Character.HandleCommand(this);
        }

        return Result;
    }

    public abstract void InitializeResult();

    ICommandResult ICommand.Execute()
    {
        return Execute();
    }
}
