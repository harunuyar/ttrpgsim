namespace DnD.CommandSystem.Results;

using DnD.CommandSystem.Commands;
using DnD.Entities;

internal class BooleanResult : ICommandResult
{
    public static BooleanResult Success(ICommand command, IDndEntity? source, bool value = false) => new BooleanResult(command, true, null, source, value);

    public static BooleanResult Failure(ICommand command, string errorMessage) => new BooleanResult(command, false, errorMessage, null, false);

    public BooleanResult(ICommand command, bool success, string? errorMsg, IDndEntity? source, bool value)
    {
        Command = command;
        IsSuccess = success;
        ErrorMessage = errorMsg;
        Source = source;
        Value = value;
    }

    public IDndEntity? Source { get; private set; }

    public bool Value { get; private set; }

    public ICommand Command { get; }

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

    public override string ToString()
    {
        return IsSuccess
            ? (Source == null ? Value.ToString() : $"{Source}: {Value}")
            : ErrorMessage ?? "Unknown error";
    }
}
