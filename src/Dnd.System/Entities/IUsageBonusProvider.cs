namespace Dnd.System.Entities;

using Dnd.System.CommandSystem.Commands;

public interface IUsageBonusProvider : IBonusProvider
{
    Task HandleUsageCommand(ICommand command);
}
