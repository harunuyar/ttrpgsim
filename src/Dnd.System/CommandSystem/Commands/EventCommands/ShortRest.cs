namespace Dnd.System.CommandSystem.Commands.EventCommands;

using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.Entities.GameActors;
using Dnd.System.Events.EventListener;

public class ShortRest : DndEventCommand
{
    public ShortRest(IEventListener eventListener, IGameActor character, TimeSpan? timeSpent) : base(eventListener, character)
    {
        TimeSpent = timeSpent ?? TimeSpan.FromMinutes(15);
    }

    public TimeSpan TimeSpent { get; }

    protected override void InitializeResult()
    {
        var passTime = new PassTime(EventListener, Actor, TimeSpent);
        var passTimeResult = passTime.Execute();

        if (!passTimeResult.IsSuccess)
        {
            SetErrorAndReturn("PassTime: " + passTimeResult.ErrorMessage);
        }
    }

    protected override void FinalizeResult()
    {
        Result.SetMessage($"{Actor.Name} have rested for {TimeSpent.TotalMinutes} minutes.");
    }
}
