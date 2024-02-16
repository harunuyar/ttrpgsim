namespace Dnd.System.CommandSystem.Commands.BaseCommands;

using Dnd.System.CommandSystem.Results;
using Dnd.System.Entities.GameActors;

public interface ICommand
{
    IGameActor Actor { get; }

    void ForceComplete();

    ICommandResult Execute();

    void AddFinalAction(Action action);
}
