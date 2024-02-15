namespace Dnd.System.CommandSystem.Results;

public class ValueResult<T> : ICommandResult
{
    public static ValueResult<T> Empty() => new ValueResult<T>(default, true, null);

    public static ValueResult<T> Success(T value) => new ValueResult<T>(value, true, null);

    public static ValueResult<T> Failure(string errorMessage) => new ValueResult<T>(default, false, errorMessage);

    private ValueResult(T? value, bool isSuccess, string? errorMsg)
    {
        Value = value;
        IsSuccess = isSuccess;
        ErrorMessage = null;
    }

    public T? Value { get; set; }

    public bool IsSuccess { get; private set; }

    public string? ErrorMessage { get; private set; }

    public void Reset()
    {
        Value = default;
    }

    public void SetError(string errorMessage)
    {
        IsSuccess = false;
        ErrorMessage = errorMessage;
    }
}
