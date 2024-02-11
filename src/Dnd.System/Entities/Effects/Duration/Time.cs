namespace Dnd.System.Entities.Effects.Duration;

using Dnd.System.CommandSystem.Commands;

public class Time : IEffectDuration
{
    public Time(int turns)
    {
        Turns = turns;
        RemainingTurns = turns;
    }

    public int Turns { get; }

    public int RemainingTurns { get; private set; }

    public void HandleCommand(DndCommand command)
    {
        // TODO: Subtract the duration of the command from the remaining duration
    }

    public bool IsExpired()
    {
        return RemainingTurns <= 0;
    }
}
