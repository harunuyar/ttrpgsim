namespace Dnd.System.CommandSystem.Commands;

using Dnd.System.CommandSystem.Results;
using Dnd.System.Entities.Characters;

public abstract class DndBooleanCommand : ICommand
{
    public DndBooleanCommand(ICharacter character)
    {
        Character = character;
        Result = BooleanResult.Empty(this);
        ShouldCollectBonuses = true;
    }

    public ICharacter Character { get; }

    public BooleanResult Result { get; }

    protected bool ShouldCollectBonuses { get; set; }

    public BooleanResult Execute()
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
