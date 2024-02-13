namespace Dnd.System.Entities.Effects.Duration;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.CommandSystem.Commands.EventCommands;
using Dnd.System.Entities.Units;

public class Time : IEffectDuration
{
    public Time(TimeSpan timeSpan)
    {
        TimeSpan = timeSpan;
    }

    public TimeSpan TimeSpan { get; }

    public void HandleCommand(ICommand command)
    {
        if (command is TakeTurn)
        {
            TimeSpan.PassTurn();
        }
        else if (command is PassTime passTime)
        {
            TimeSpan.PassTime(passTime.TimeSpan);
        }
    }

    public bool IsExpired()
    {
        return TimeSpan.IsOver;
    }
}
