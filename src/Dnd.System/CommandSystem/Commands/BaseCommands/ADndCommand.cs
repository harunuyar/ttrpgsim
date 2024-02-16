namespace Dnd.System.CommandSystem.Commands.BaseCommands;

using Dnd.System.CommandSystem.Results;
using Dnd.System.Entities.GameActors;
using global::System;

public abstract class ADndCommand<T> : ICommand where T : ICommandResult
{
    public ADndCommand(IGameActor character)
    {
        Actor = character;
        FinalActions = new List<Action>();
        ShouldVisitEntities = true;
    }

    public IGameActor Actor { get; }

    protected bool ShouldVisitEntities { get; set; }

    public bool IsForceCompleted { get; private set; }

    private List<Action> FinalActions { get; }

    public abstract T Result { get; }

    public void AddFinalAction(Action action)
    {
        FinalActions.Add(action);
    }

    public void ForceComplete()
    {
        IsForceCompleted = true;
    }

    public T Execute()
    {
        Result.Reset();

        FirstAction();

        InitializeResult();

        if (!IsForceCompleted && ShouldVisitEntities && Result.IsSuccess)
        {
            Actor.HandleCommand(this);
        }

        foreach (Action action in FinalActions)
        {
            if (!IsForceCompleted)
            {
                action();
            }
        }

        if (!IsForceCompleted)
        {
            FinalizeResult();
        }

        FinalAction();

        return Result;
    }

    protected virtual void InitializeResult() { }

    protected virtual void FinalizeResult() { }

    protected virtual void FirstAction() { }

    protected virtual void FinalAction() { }

    ICommandResult ICommand.Execute()
    {
        return Execute();
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
