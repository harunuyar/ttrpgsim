namespace Dnd.CommandSystem.Results;

using Dnd.CommandSystem.Commands;

public interface ICommandResult
{
    ICommand Command { get; }

    bool IsSuccess { get; }

    string? ErrorMessage { get; }
}
