namespace DnD.CommandSystem.Results;

using TableTopRpg.Commands;

internal class DndCommandResult : ICommandResult
{
    public DndCommandResult(ICommand command, bool success = true, string? errorMsg = null)
    {
        Command = command;
        IsSuccess = success;
        ErrorMessage = errorMsg;
    }

    public ICommand Command { get; }

    public bool IsSuccess { get; private set; }

    public string? ErrorMessage { get; private set; }

    public void SetError(string errorMessage)
    {
        IsSuccess = false;
        ErrorMessage = errorMessage;
    }
}
