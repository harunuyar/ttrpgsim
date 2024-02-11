namespace Dnd.CommandSystem.Commands;

using Dnd.CommandSystem.Results;

public interface ICommand
{
    ICommandResult Execute();
}
