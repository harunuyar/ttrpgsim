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
            foreach (var trait in Actor.Race.Traits)
            {
                if (!IsForceCompleted)
                {
                    await trait.HandleCommand(this);
                }
            }

            foreach (var trait in Actor.Subrace?.RacialTraits ?? [])
            {
                if (!IsForceCompleted)
                {
                    await trait.HandleCommand(this);
                }
            }

            foreach (var level in Actor.LevelInfo.GetLevels())
            {
                foreach (var feature in level.Features)
                {
                    if (!IsForceCompleted)
                    {
                        await feature.HandleCommand(this);
                    }
                }
            }

            foreach (var equipment in Actor.Inventory.EquipedItems)
            {
                if (!IsForceCompleted)
                {
                    await equipment.HandleCommand(this);
                }
            }

            foreach (var effect in Actor.EffectsTable.ActiveEffects)
            {
                if (!IsForceCompleted)
                {
                    await effect.HandleCommand(this);
                }
            }

            foreach (var effect in Actor.EffectsTable.CausedEffects)
            {
                if (!IsForceCompleted)
                {
                    await effect.HandleCommand(this);
                }
            }
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
