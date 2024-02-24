namespace Dnd.System.CommandSystem.Results;

public class ValueResult<T> : ICommandResult
{
    public static ValueResult<T> Empty()
    {
        return new ValueResult<T>(true, string.Empty, default);
    }

    public static ValueResult<T> Success(string message, T value)
    {
        return new ValueResult<T>(true, message, value);
    }

    public static ValueResult<T> Failure(string errorMessage)
    {
        return new ValueResult<T>(false, errorMessage, default);
    }

    protected ValueResult(bool isSuccess, string message, T? value)
    {
        Value = value;
        IsSuccess = isSuccess;
        Message = message;
    }

    public string Message { get; private set; }

    public T? Value { get; private set; }

    public bool IsSuccess { get; private set; }

    public string? ErrorMessage => IsSuccess ? null : Message;

    internal void Set(ValueResult<T> result)
    {
        Message = result.Message;
        Value = result.Value;
        IsSuccess = result.IsSuccess;
    }

    internal void SetValue(string message, T value)
    {
        Message = message;
        Value = value;
        IsSuccess = true;
    }

    public void SetError(string errorMessage)
    {
        Message = errorMessage;
        IsSuccess = false;
    }

    public void Reset()
    {
        Message = string.Empty;
        Value = default;
        IsSuccess = true;
    }
}
