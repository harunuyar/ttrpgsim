namespace Dnd.System.CommandSystem.Commands;

using Dnd.System.CommandSystem.Results;

public interface ICommand
{
    ICommandResult Execute();
}
