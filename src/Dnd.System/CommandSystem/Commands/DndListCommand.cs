namespace Dnd.System.CommandSystem.Commands;

using Dnd.System.CommandSystem.Results;
using Dnd.System.Entities.GameActors;

public abstract class DndListCommand<T> : ICommand
{
    public DndListCommand(IGameActor character)
    {
        Character = character;
        Result = new ListResult<T>();
        ShouldVisitEntities = true;
    }

    public IGameActor Character { get; }

    protected bool ShouldVisitEntities { get; set; }

    public bool IsForceCompleted { get; private set; }

    protected ListResult<T> Result { get; }

    public ListResult<T> Execute()
    {
        Result.Values.Clear();

        InitializeEvent();

        if (!IsForceCompleted && ShouldVisitEntities && Result.IsSuccess)
        {
            Character.HandleCommand(this);
        }

        if (!IsForceCompleted)
        {
            FinalizeEvent();
        }

        return Result;
    }

    protected virtual void InitializeEvent() { }

    protected virtual void FinalizeEvent() { }

    ICommandResult ICommand.Execute()
    {
        return Execute();
    }

    public void ForceComplete()
    {
        IsForceCompleted = true;
    }

    public void AddItem(T item)
    {
        if (!IsForceCompleted)
        {
            Result.Values.Add(item);
        }
    }

    public void SetErrorAndReturn(string errorMessage)
    {
        if (!IsForceCompleted)
        {
            Result.Values.Clear();
            Result.SetError(errorMessage);
            ForceComplete();
        }
    }
}
