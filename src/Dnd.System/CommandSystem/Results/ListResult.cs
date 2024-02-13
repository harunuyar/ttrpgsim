namespace Dnd.System.CommandSystem.Results;

public class ListResult<T> : ICommandResult
{
    public ListResult()
    {
        IsSuccess = true;
        Values = new List<T>();
    }

    public List<T> Values { get; }

    public bool IsSuccess { get; private set; }

    public string? ErrorMessage { get; private set; }

    public void SetError(string errorMessage)
    {
        IsSuccess = false;
        ErrorMessage = errorMessage;
    }
}
