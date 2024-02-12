namespace Dnd.System.CommandSystem.Commands;

using Dnd.System.CommandSystem.Results;
using Dnd.System.Entities.Characters;

public interface ICommand
{
    ICharacter Character { get; }

    ICommandResult Execute();
}
