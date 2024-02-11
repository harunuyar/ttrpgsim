namespace Dnd.System.Entities.Effects.Duration;

using Dnd.System.CommandSystem.Commands;

public class Concentration : IEffectDuration
{
    public Concentration(Time? timeDuration)
    {
        IsConcentrationBroken = false;
        TimeDuration = timeDuration;
    }

    private bool IsConcentrationBroken { get; set; } 

    private Time? TimeDuration { get; }

    public void HandleCommand(DndCommand command)
    {
        // TODO: Check if the command is ConcentrationBrokenEvent

        TimeDuration?.HandleCommand(command);
        if (TimeDuration?.IsExpired() == true)
        {
            IsConcentrationBroken = true;
        }
    }

    public bool IsExpired()
    {
        return IsConcentrationBroken;
    }
}
