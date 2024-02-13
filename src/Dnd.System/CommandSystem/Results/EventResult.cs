namespace Dnd.System.CommandSystem.Results;

public class EventResult : ICommandResult
{
    public static EventResult Success(string? message = null) => new EventResult(true, message);

    public static EventResult Failure(string errorMessage) => new EventResult(false, errorMessage);

    private EventResult(bool success = true, string? message = null)
    {
        IsSuccess = success;
        Message = message;
    }

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

    public void Reset()
    {
        IsSuccess = true;
        Message = null;
    }
}