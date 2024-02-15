namespace Dnd.System.CommandSystem.Results;

using Dnd.System.Entities;

public class BooleanResult : ICommandResult
{
    public static BooleanResult Empty() => new BooleanResult(true, null, null, false);

    public static BooleanResult Success(string source, bool value) => new BooleanResult(true, null, new CustomDndEntity(source), value);

    public static BooleanResult Success(IDndEntity source, bool value) => new BooleanResult(true, null, source, value);

    public static BooleanResult Failure(string errorMessage) => new BooleanResult(false, errorMessage, null, false);

    public BooleanResult(bool success, string? errorMsg, IDndEntity? source, bool value)
    {
        IsSuccess = success;
        Message = errorMsg;
        Source = source;
        Value = value;
    }

    public IDndEntity? Source { get; private set; }

    public bool Value { get; private set; }

    public string? Message { get; private set; }

    public bool IsSuccess { get; private set; }

    public string? ErrorMessage => IsSuccess ? null : Message;

    public void SetError(string errorMessage)
    {
        IsSuccess = false;
        Message = errorMessage;
    }

    public void SetValue(IDndEntity? source, bool value, string message)
    {
        Source = source;
        Value = value;
        Message = message ?? string.Empty;
    }

    public void SetValue(bool value, string message)
    {
        SetValue(null, value, message);
    }

    public void Reset()
    {
        IsSuccess = true;
        Message = null;
        Source = null;
        Value = false;
    }

    public override string ToString()
    {
        return IsSuccess ? $"{Source}: {Message}. {Value}" : ErrorMessage ?? "Unknown error";
    }
}
