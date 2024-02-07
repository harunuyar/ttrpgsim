namespace DnD.CommandSystem.CommandResult;

using DnD.CommandSystem.Commands;

internal interface ICommandResult
{
    ICommand Command { get; }
    bool IsSuccess { get; }
    string Message { get; }
}
