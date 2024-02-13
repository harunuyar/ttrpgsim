namespace Dnd.System.CommandSystem.Results;

using Dnd.System.Entities;

public class BooleanResult : ICommandResult
{
    public static BooleanResult Empty() => new BooleanResult(true, null, null, false);

    public static BooleanResult Success(IDndEntity source, bool value) => new BooleanResult(true, null, source, value);

    public static BooleanResult Failure(string errorMessage) => new BooleanResult(false, errorMessage, null, false);

    public BooleanResult(bool success, string? errorMsg, IDndEntity? source, bool value)
    {
        IsSuccess = success;
        ErrorMessage = errorMsg;
        Source = source;
        Value = value;
    }

    public IDndEntity? Source { get; private set; }

    public bool Value { get; private set; }

    public bool IsSuccess { get; private set; }

    public string? ErrorMessage { get; private set; }

    public void SetError(string errorMessage)
    {
        IsSuccess = false;
        ErrorMessage = errorMessage;
    }

    public void SetValue(IDndEntity source, bool value)
    {
        Source = source;
        Value = value;
    }

    public void SetValue(string source, bool value)
    {
        SetValue(new CustomDndEntity(source), value);
    }

    public void Reset()
    {
        IsSuccess = true;
        ErrorMessage = null;
        Source = null;
        Value = false;
    }

    public override string ToString()
    {
        return IsSuccess
            ? (Source == null ? Value.ToString() : $"{Source}: {Value}")
            : ErrorMessage ?? "Unknown error";
    }
}
