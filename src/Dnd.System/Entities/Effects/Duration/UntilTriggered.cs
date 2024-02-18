namespace Dnd.System.Entities.Effects.Duration;

using Dnd.System.CommandSystem.Commands.BaseCommands;

public class UntilTriggered : IEffectDuration
{
    public UntilTriggered(int times)
    {
        Times = times;
        TimesTriggered = 0;
    }

    public int Times { get; set; }

    public int TimesTriggered { get; set; }

    public void Trigger()
    {
        TimesTriggered++;
    }

    public void HandleCommand(ICommand command)
    {
        // TODO: Check if the command is TriggeredEvent
    }

    public bool IsExpired()
    {
        return TimesTriggered >= Times;
    }
}
