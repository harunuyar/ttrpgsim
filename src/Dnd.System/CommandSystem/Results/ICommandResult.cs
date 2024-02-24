namespace Dnd.System.CommandSystem.Results;

using Dnd.System.CommandSystem.Commands;

public interface ICommandResult
{
    bool IsSuccess { get; }
    string? ErrorMessage { get; }
    void SetError(string errorMessage);
    void Reset();
}
