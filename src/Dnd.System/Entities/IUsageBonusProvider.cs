namespace Dnd.System.Entities;

using Dnd.System.CommandSystem.Commands;

public interface IUsageBonusProvider : ICommandHandler
{
    Task HandleUsageCommand(ICommand command);
}
