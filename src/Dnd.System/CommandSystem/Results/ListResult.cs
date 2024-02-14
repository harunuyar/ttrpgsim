namespace Dnd.System.CommandSystem.Results;

public class ListResult<T> : ICommandResult
{
    public ListResult()
    {
        IsSuccess = true;
        Values = new List<T>();
    }

    public List<T> Values { get; private set; }

    public bool IsSuccess { get; private set; }

    public string? ErrorMessage { get; private set; }

    public void Set(List<T> values)
    {
        IsSuccess = true;
        Values.Clear();
        Values.AddRange(values);
    }

    public void Reset()
    {
        IsSuccess = true;
        Values.Clear();
    }

    public void SetError(string errorMessage)
    {
        IsSuccess = false;
        ErrorMessage = errorMessage;
    }
}
