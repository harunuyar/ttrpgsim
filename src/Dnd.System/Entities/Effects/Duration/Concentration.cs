namespace Dnd.System.Entities.Effects.Duration;

using Dnd.System.CommandSystem.Commands.BaseCommands;

public class Concentration : IEffectDuration
{
    public Concentration(Time? timeDuration)
    {
        IsConcentrationBroken = false;
        TimeDuration = timeDuration;
    }

    private bool IsConcentrationBroken { get; set; } 

    private Time? TimeDuration { get; }

    public void HandleCommand(ICommand command)
    {
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
