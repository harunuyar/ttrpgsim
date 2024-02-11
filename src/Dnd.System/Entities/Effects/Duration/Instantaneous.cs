namespace Dnd.System.Entities.Effects.Duration;

using Dnd.System.CommandSystem.Commands;

internal class Instantaneous : IEffectDuration
{
    public Instantaneous()
    {
        HasTriggered = false;
    }

    private bool HasTriggered { get; set; } 

    public void HandleCommand(DndCommand command)
    {
        // TODO: Check if the command is CreatedEffectEvent
    }

    public bool IsExpired()
    {
        return HasTriggered;
    }
}
