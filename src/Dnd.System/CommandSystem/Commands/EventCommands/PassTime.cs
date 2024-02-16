namespace Dnd.System.CommandSystem.Commands.EventCommands;

using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.Entities.GameActors;
using Dnd.System.Events.EventListener;

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
