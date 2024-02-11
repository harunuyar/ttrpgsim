namespace Dnd.System.CommandSystem.Results;

using Dnd.System.CommandSystem.Commands;

public interface ICommandResult
{
    ICommand Command { get; }

    bool IsSuccess { get; }

    string? ErrorMessage { get; }
}
