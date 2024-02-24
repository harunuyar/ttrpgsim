namespace Dnd.System.CommandSystem.Results;

public class ListResult<T> : ICommandResult
{
    public static ListResult<T> Empty()
    {
        return new ListResult<T>(true, null, []);
    }

    public static ListResult<T> Success(List<(string, T)> values)
    {
        return new ListResult<T>(true, null, values);
    }

    public static ListResult<T> Failure(string errorMessage)
    {
        return new ListResult<T>(false, errorMessage, []);
    }

    protected ListResult(bool isSuccess, string? errorMessage, List<(string, T)> values)
    {
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage;
        Values = values;
    }

    public List<(string, T)> Values { get; private set; }

    public bool IsSuccess { get; private set; }

    public string? ErrorMessage { get; private set; }

    internal void Set(ListResult<T> result)
    {
        Values = [.. result.Values];
        IsSuccess = result.IsSuccess;
        ErrorMessage = result.ErrorMessage;
    }

    internal void Add(ListResult<T> result)
    {
        Values.AddRange(result.Values);
    }

    public void SetError(string errorMessage)
    {
        IsSuccess = false;
        ErrorMessage = errorMessage;
        Values.Clear();
    }

    internal void AddValue(string message, T value)
    {
        Values.Add((message, value));
    }

    internal void AddValues(string message, IEnumerable<T> values)
    {
        Values.AddRange(values.Select(v => (message, v)));
    }

    internal void SetValues(List<(string, T)> values)
    {
        Values = values;
    }

    public void Reset()
    {
        IsSuccess = true;
        ErrorMessage = null;
        Values.Clear();
    }
}
