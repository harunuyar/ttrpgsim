namespace Dnd.System.Entities.Effects.Duration;

using Dnd.System.CommandSystem.Commands;

public interface IEffectDuration
{
    void HandleCommand(ICommand command);

    bool IsExpired();
}
