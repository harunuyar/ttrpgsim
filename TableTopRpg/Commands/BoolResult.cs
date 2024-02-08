namespace TableTopRpg.Commands;

public class BoolResult : ICommandResult
{
    public static BoolResult Success(ICommand command, bool value) => new BoolResult(command, true, null, value);
    public static BoolResult Failure(ICommand command, string? message = null) => new BoolResult(command, false, message, false);

    private BoolResult(ICommand command, bool success, string? message, bool value)
    {
        Command = command;
        IsSuccess = success;
        Message = message ?? (success ? value.ToString() : "FAILURE");
        Value = value;
    }

    public ICommand Command { get; }

    public bool Value { get; }

    public bool IsSuccess { get; }

    public string Message { get; }
}
