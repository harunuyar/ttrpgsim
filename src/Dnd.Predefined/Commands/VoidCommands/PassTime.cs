namespace Dnd.Predefined.Commands.VoidCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.GameActor;

public class PassTime : VoidCommand
{
    public PassTime(IGameActor character, TimeSpan timeSpan) : base(character)
    {
        TimeSpan = timeSpan;
    }

    public TimeSpan TimeSpan { get; }

    protected override Task FinalizeResult()
    {
        SetMessage($"{TimeSpan} has passed.");

        return Task.CompletedTask;
    }
}
