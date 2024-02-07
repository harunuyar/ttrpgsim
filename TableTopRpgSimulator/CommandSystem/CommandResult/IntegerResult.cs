namespace DnD.CommandSystem.CommandResult;

using DnD.CommandSystem.Commands;

internal class IntegerResult : ICommandResult
{
    public static IntegerResult Success(ICommand command, int value) => new IntegerResult(command, true, null, value);
    public static IntegerResult Failure(ICommand command, string? message = null) => new IntegerResult(command, false, message, 0);

    private IntegerResult(ICommand command, bool success, string? message, int value)
    {
        Command = command;
        IsSuccess = success;
        Message = message ?? (success ? value.ToString() : "FAILURE");
        Value = value;
    }

    public ICommand Command { get; }

    public int Value { get; }

    public bool IsSuccess { get; }

    public string Message { get; }
}
