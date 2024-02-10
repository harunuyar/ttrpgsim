namespace DnD.CommandSystem.Commands;

using DnD.CommandSystem.Results;

public interface ICommand
{
    ICommandResult Execute();
}
