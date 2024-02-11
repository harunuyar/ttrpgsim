namespace Dnd.System.Entities.Effects.Duration;

using Dnd.System.CommandSystem.Commands;

public interface IEffectDuration
{
    void HandleCommand(DndCommand command);

    bool IsExpired();
}
