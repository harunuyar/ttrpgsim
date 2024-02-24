namespace Dnd.System.CommandSystem.Commands;

using Dnd.System.CommandSystem.Results;
using Dnd.System.Entities.GameActor;

public interface ICommand
{
    IGameActor Actor { get; }

    void ForceComplete();

    Task<ICommandResult> Execute();

    void AddFinalAction(Action action);
}
