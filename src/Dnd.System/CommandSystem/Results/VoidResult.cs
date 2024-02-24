namespace Dnd.System.CommandSystem.Results;

public class VoidResult : ICommandResult
{
    public static VoidResult Empty() => new(true, string.Empty);

    public static VoidResult Failure(string errorMessage) => new(false, errorMessage);

    private VoidResult(bool isSuccess, string message)
    {
        IsSuccess = isSuccess;
        Message = message;
    }

    public string Message { get; private set; }

    public bool IsSuccess { get; private set; }

    public string? ErrorMessage => IsSuccess ? null : Message;

    public void SetError(string errorMessage)
    {
        Message = errorMessage;
        IsSuccess = false;
    }

    internal void SetMessage(string message)
    {
        Message = message;
        IsSuccess = true;
    }

    public void Reset()
    {
        IsSuccess = true;
        Message = string.Empty;
    }
}
