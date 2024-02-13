namespace Dnd.System.CommandSystem.Commands;

using Dnd.System.CommandSystem.Results;
using Dnd.System.Entities;
using Dnd.System.Entities.Advantage;
using Dnd.System.Entities.GameActors;

public abstract class DndScoreCommand : ICommand
{
    public DndScoreCommand(IGameActor character)
    {
        Character = character;
        Result = IntegerResultWithBonus.Empty();
        ShouldVisitEntities = true;
    }

    public IGameActor Character { get; }

    protected IntegerResultWithBonus Result { get; }

    protected bool ShouldVisitEntities { get; set; }

    public bool IsForceCompleted { get; private set; }

    public IntegerResultWithBonus Execute()
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

    public void AddBonus(IBonusProvider bonusProvider, int value)
    {
        if (!IsForceCompleted)
        {
            Result.BonusCollection.AddBonus(bonusProvider, value);
        }
    }

    public void AddAdvantage(IBonusProvider bonusProvider, EAdvantage value)
    {
        if (!IsForceCompleted)
        {
            Result.BonusCollection.AddAdvantage(bonusProvider, value);
        }
    }

    public void SetBaseValue(IBonusProvider bonusProvider, int value)
    {
        if (!IsForceCompleted)
        {
            Result.SetBaseValue(bonusProvider, value);
        }
    }

    public void SetValueAndReturn(IBonusProvider bonusProvider, int value)
    {
        if (!IsForceCompleted)
        {
            Result.SetBaseValue(bonusProvider, value);
            Result.BonusCollection.Values.Clear();
            Result.BonusCollection.Advantages.Clear();
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

    public IntegerResultWithBonus GetResult()
    {
        return Result;
    }
}
