namespace Dnd.System.Entities.Effects.Duration;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.CommandSystem.Commands.EventCommands;

public class Time : IEffectDuration
{
    public Time(int turns)
    {
        Turns = turns;
        RemainingTurns = turns;
    }

    public int Turns { get; }

    public int RemainingTurns { get; private set; }

    public void HandleCommand(ICommand command)
    {
        if (command is TakeTurn)
        {
            RemainingTurns--;
        }
    }

    public bool IsExpired()
    {
        return RemainingTurns <= 0;
    }
}
