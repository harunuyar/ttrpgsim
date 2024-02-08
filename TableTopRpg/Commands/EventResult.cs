namespace TableTopRpg.Commands;

public class EventResult : ICommandResult
{
    public static EventResult Success(ICommand command, string? message = null) => new EventResult(command, true, message);
    public static EventResult Failure(ICommand command, string? message = null) => new EventResult(command, false, message);

    private EventResult(ICommand command, bool isSuccess, string? message)
    {
        Command = command;
        IsSuccess = isSuccess;
        Message = message ?? (isSuccess ? "SUCCESS" : "FAILURE");
    }

    public ICommand Command { get; }

    public bool IsSuccess { get; }

    public string Message { get; }
}
