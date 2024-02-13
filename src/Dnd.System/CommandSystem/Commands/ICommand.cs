namespace Dnd.System.CommandSystem.Commands;

using Dnd.System.CommandSystem.Results;
using Dnd.System.Entities.GameActors;

public interface ICommand
{
    IGameActor Character { get; }

    bool IsForceCompleted { get; }

    void ForceComplete();

    ICommandResult Execute();
}
