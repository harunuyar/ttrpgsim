namespace TableTopRpg.Commands;

public class IntegerResult : ICommandResult
{
    public static IntegerResult Success(ICommand command, int value, string? message = null) => new IntegerResult(command, true, message, value);
    public static IntegerResult Failure(ICommand command, string? message = null) => new IntegerResult(command, false, message, 0);

    protected IntegerResult(ICommand command, bool success, string? message, int value)
    {
        Command = command;
        IsSuccess = success;
        Message = message ?? (success ? value.ToString() : "FAILURE");
        Value = value;
    }

    public ICommand Command { get; }

    public int Value { get; set; }

    public bool IsSuccess { get; set; }

    public string Message { get; set; }
}
