namespace DnD.CommandSystem.Results;

using TableTopRpg.Commands;

internal class EventResult : DndCommandResult
{
    public static EventResult Success(ICommand command) => new EventResult(command);
    public static EventResult Failure(ICommand command, string errorMessage) => new EventResult(command, false, errorMessage);

    private EventResult(ICommand command, bool success = true, string? errorMsg = null) : base(command, success, errorMsg)
    {
    }
}