namespace Dnd.CommandSystem.Results;

using Dnd.CommandSystem.Commands;

public class EventResult : ICommandResult
{
    public static EventResult Success(ICommand command) => new EventResult(command);

    public static EventResult Failure(ICommand command, string errorMessage) => new EventResult(command, false, errorMessage);

    private EventResult(ICommand command, bool success = true, string? errorMsg = null)
    {
        Command = command;
        IsSuccess = success;
        ErrorMessage = errorMsg;
    }

    public ICommand Command { get; }

    public bool IsSuccess { get; private set; }

    public string? ErrorMessage { get; private set; }

    public void SetError(string errorMessage)
    {
        IsSuccess = false;
        ErrorMessage = errorMessage;
    }
}