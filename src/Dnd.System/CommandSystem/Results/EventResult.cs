namespace Dnd.System.CommandSystem.Results;

using Dnd.System.CommandSystem.Commands;

public class EventResult : ICommandResult
{
    public static EventResult Success(ICommand command, string? message = null) => new EventResult(command, true, message);

    public static EventResult Failure(ICommand command, string errorMessage) => new EventResult(command, false, errorMessage);

    private EventResult(ICommand command, bool success = true, string? message = null)
    {
        Command = command;
        IsSuccess = success;
        Message = message;
    }

    public ICommand Command { get; }

    public bool IsSuccess { get; private set; }

    public string? Message { get; private set; }

    public string? ErrorMessage => IsSuccess ? null : Message;

    public void SetMessage(string message)
    {
        Message = message;
    }

    public void SetError(string errorMessage)
    {
        IsSuccess = false;
        
        Message = errorMessage;
    }
}