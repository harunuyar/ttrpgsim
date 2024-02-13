using Dnd.System.CommandSystem.Commands;

namespace Dnd.System.CommandSystem.Results;

public class ListResult<T> : ICommandResult
{
    public ListResult(ICommand command)
    {
        Command = command;
        IsSuccess = true;
        Values = new List<T>();
    }

    public ICommand Command { get; }

    public List<T> Values { get; }

    public bool IsSuccess { get; private set; }

    public string? ErrorMessage { get; private set; }

    public void SetError(string errorMessage)
    {
        IsSuccess = false;
        ErrorMessage = errorMessage;
    }
}
