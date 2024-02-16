namespace Dnd.System.CommandSystem.Commands.EventCommands;

using Dnd.System.Entities.GameActors;

public class PassTime : DndEventCommand
{
    public PassTime(IEventListener eventListener, IGameActor character, TimeSpan timeSpan) : base(eventListener, character)
    {
        TimeSpan = timeSpan;
    }

    public TimeSpan TimeSpan { get;}

    protected override void FinalizeResult()
    {
        Result.SetMessage($"{TimeSpan} has passed.");
    }
}
