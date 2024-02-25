namespace Dnd.System.CommandSystem.Commands;

using Dnd.System.CommandSystem.Results;
using Dnd.System.Entities.GameActor;

public class ScoreCommand : ACommand<ScoreResult>
{
    public ScoreCommand(IGameActor actor) : base(actor)
    {
        Result = ScoreResult.Empty();
    }

    protected override ScoreResult Result { get; }

    public int GetCurrentValue()
    {
        return Result.Value;
    }

    public void SetBaseValue(int value, string message)
    {
        if (IsForceCompleted)
        {
            return;
        }

        Result.SetBaseValue(message, value);
    }

    public void SetBaseValueAndReturn(int value, string message)
    {
        if (IsForceCompleted)
        {
            return;
        }

        Result.SetBaseValue(message, value);
        Result.Bonus.Reset();
        ForceComplete();
    }

    public void Set(ScoreResult other)
    {
        if (IsForceCompleted)
        {
            return;
        }

        Result.Set(other);
    }

    public void AddBonus(int value, string message)
    {
        if (IsForceCompleted)
        {
            return;
        }

        Result.AddBonus(message, value);
    }

    public void AddBonuses(IEnumerable<int> bonuses, string message)
    {
        if (IsForceCompleted)
        {
            return;
        }

        Result.AddBonuses(message, bonuses);
    }

    public void AddBonus(ListResult<int> other)
    {
        if (IsForceCompleted)
        {
            return;
        }

        Result.AddBonus(other);
    }

    public void AddBonus(ScoreResult other)
    {
        if (IsForceCompleted)
        {
            return;
        }

        Result.AddBonus(other);
    }
}
