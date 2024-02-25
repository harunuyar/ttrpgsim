namespace Dnd.System.CommandSystem.Commands;

using Dnd.System.CommandSystem.Results;
using Dnd.System.Entities.GameActor;

public abstract class ACommand<T> : ICommand where T : ICommandResult
{
    private readonly List<Action> finalActions;

    public ACommand(IGameActor actor)
    {
        Actor = actor;
        finalActions = [];
    }

    protected abstract T Result { get; }

    public IGameActor Actor { get; }

    public bool IsForceCompleted { get; private set; }

    protected bool ShouldVisitTraitsAndFeatures => !IsForceCompleted && Result.IsSuccess;

    public async Task<T> Execute()
    {
        Result.Reset();

        await FirstAction();

        await InitializeResult();

        if (!IsForceCompleted && Result.IsSuccess)
        {
            await Actor.HandleCommand(this);

            /*if (!IsForceCompleted)
            {
                await Actor.Race.HandleCommand(this);
            }

            if (!IsForceCompleted && Actor.Subrace is not null)
            {
                await Actor.Subrace.HandleCommand(this);
            }

            foreach (var c in Actor.LevelInfo.GetClasses())
            {
                if (!IsForceCompleted)
                {
                    await c.HandleCommand(this);
                }
            }

            foreach (var level in Actor.LevelInfo.GetLevels())
            {
                if (!IsForceCompleted)
                {
                    await level.HandleCommand(this);
                }
            }

            if (!IsForceCompleted)
            {
                await Actor.SpellMemory.HandleCommand(this);
            }

            if (!IsForceCompleted)
            {
                await Actor.ActionCounter.HandleCommand(this);
            }

            if (!IsForceCompleted)
            {
                await Actor.Inventory.HandleCommand(this);
            }

            if (!IsForceCompleted)
            {
                await Actor.EffectsTable.HandleCommand(this);
            }

            if (!IsForceCompleted)
            {
                await Actor.HitPoints.HandleCommand(this);
            }

            if (!IsForceCompleted)
            {
                await Actor.Alignment.HandleCommand(this);
            }*/
        }

        foreach (Action action in finalActions)
        {
            if (!IsForceCompleted)
            {
                action();
            }
        }

        if (!IsForceCompleted)
        {
            await FinalizeResult();
        }

        await FinalAction();

        return Result;
    }

    protected virtual Task InitializeResult() { return Task.CompletedTask; }

    protected virtual Task FinalizeResult() { return Task.CompletedTask; }

    protected virtual Task FirstAction() { return Task.CompletedTask; }

    protected virtual Task FinalAction() { return Task.CompletedTask; }

    async Task<ICommandResult> ICommand.Execute()
    {
        return await Execute();
    }

    public void AddFinalAction(Action action)
    {
        if (IsForceCompleted)
        {
            return;
        }

        finalActions.Add(action);
    }

    public void ForceComplete()
    {
        IsForceCompleted = true;
    }

    public void SetError(string error)
    {
        Result.SetError(error);
        IsForceCompleted = true;
    }
}
