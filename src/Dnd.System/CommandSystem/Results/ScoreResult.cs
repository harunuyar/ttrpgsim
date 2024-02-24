namespace Dnd.System.CommandSystem.Results;

public class ScoreResult : ICommandResult
{
    public static ScoreResult Empty()
    {
        return new ScoreResult(true, string.Empty, 0);
    }

    public static ScoreResult Success(string message, int value)
    {
        return new ScoreResult(true, message, value);
    }

    public static ScoreResult Failure(string errorMessage)
    {
        return new ScoreResult(false, errorMessage, default);
    }

    protected ScoreResult(bool isSuccess, string message, int value)
    {
        Bonus = ListResult<int>.Empty();
        BaseValue = value;
        IsSuccess = isSuccess;
        Message = message;
    }

    public string Message { get; private set; }

    public int BaseValue { get; private set; }

    public int Value => BaseValue + Bonus.Values.Sum(v => v.Item2);

    public bool IsSuccess { get; private set; }

    public string? ErrorMessage => IsSuccess ? null : Message;

    public ListResult<int> Bonus { get; }

    internal void AddBonus(string message, int value)
    {
        Bonus.AddValue(message, value);
    }

    internal void AddBonus(ListResult<int> bonus)
    {
        Bonus.Add(bonus);
    }

    internal void AddBonuses(string message, IEnumerable<int> bonuses)
    {
        Bonus.AddValues(message, bonuses);
    }

    internal void AddBonus(ScoreResult other)
    {
        if (other.IsSuccess)
        {
            Bonus.AddValue(other.Message, other.BaseValue);
            Bonus.Add(other.Bonus);
        }
    }

    internal void Set(ScoreResult result)
    {
        Bonus.Set(result.Bonus);
        Message = result.Message;
        BaseValue = result.BaseValue;
        IsSuccess = result.IsSuccess;
    }

    internal void SetBaseValue(string message, int value)
    {
        Message = message;
        BaseValue = value;
        IsSuccess = true;
    }

    public void SetError(string errorMessage)
    {
        Message = errorMessage;
        IsSuccess = false;
    }

    public void Reset()
    {
        Bonus.Reset();
        Message = string.Empty;
        BaseValue = default;
        IsSuccess = true;
    }
}
