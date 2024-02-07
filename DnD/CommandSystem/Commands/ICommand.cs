using DnD.CommandSystem.CommandResult;

namespace DnD.CommandSystem.Commands;

internal interface ICommand
{
    ICommandResult Execute();
}
