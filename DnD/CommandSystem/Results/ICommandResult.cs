namespace DnD.CommandSystem.Results;

using DnD.CommandSystem.Commands;

public interface ICommandResult
{
    ICommand Command { get; }

    bool IsSuccess { get; }

    string? ErrorMessage { get; }
}
