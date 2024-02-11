namespace Dnd.System.Entities.Effects.Duration;

using Dnd.System.CommandSystem.Commands;

public class UntilDispelled : IEffectDuration
{
    public UntilDispelled()
    {
        IsDispelled = false;
    }

    private bool IsDispelled { get; set; }

    public void HandleCommand(DndCommand command)
    {
        // TODO: Check if the command is DispelledEvent
    }

    public bool IsExpired()
    {
        return IsDispelled;
    }
}
