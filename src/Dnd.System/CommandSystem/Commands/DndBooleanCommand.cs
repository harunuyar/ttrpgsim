namespace Dnd.System.CommandSystem.Commands;

using Dnd.System.CommandSystem.Results;
using Dnd.System.Entities;
using Dnd.System.Entities.GameActors;

public abstract class DndBooleanCommand : ICommand
{
    public DndBooleanCommand(IGameActor character)
    {
        Character = character;
        Result = BooleanResult.Empty();
        ShouldVisitEntities = true;
    }

    public IGameActor Character { get; }

    protected BooleanResult Result { get; }

    protected bool ShouldVisitEntities { get; set; }

    public bool IsForceCompleted { get; private set; }

    public BooleanResult Execute()
    {
        Result.Reset();

        InitializeResult();

        if (!IsForceCompleted && ShouldVisitEntities && Result.IsSuccess)
        {
            Character.HandleCommand(this);
        }

        if (!IsForceCompleted)
        {
            FinalizeResult();
        }
        
        return Result;
    }

    protected abstract void InitializeResult();

    protected abstract void FinalizeResult();

    ICommandResult ICommand.Execute()
    {
        return Execute();
    }

    public void ForceComplete()
    {
        IsForceCompleted = true;
    }

    public void SetValue(IBonusProvider bonusProvider, bool value)
    {
        if (!IsForceCompleted)
        {
            Result.SetValue(bonusProvider, value);
        }
    }

    public void SetValueAndReturn(IBonusProvider bonusProvider, bool value)
    {
        if (!IsForceCompleted)
        {
            Result.SetValue(bonusProvider, value);
            ForceComplete();
        }
    }

    public void SetErrorAndReturn(string errorMessage)
    {
        if (!IsForceCompleted)
        {
            Result.SetError(errorMessage);
            ForceComplete();
        }
    }
}
