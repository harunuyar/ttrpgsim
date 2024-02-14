namespace Dnd.System.CommandSystem.Results;

using Dnd.System.CommandSystem.Commands;

public interface ICommandResult
{
    bool IsSuccess { get; }

    string? ErrorMessage { get; }

    void Reset();

    void SetError(string errorMessage);
}
