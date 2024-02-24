namespace Dnd.Predefined.Commands.VoidCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.GameActor;

public class ShortRest : VoidCommand
{
    public ShortRest(IGameActor character, TimeSpan? timeSpent) : base(character)
    {
        TimeSpent = timeSpent ?? TimeSpan.FromMinutes(15);
    }

    public TimeSpan TimeSpent { get; }

    protected override async Task InitializeResult()
    {
        var passTime = await new PassTime(Actor, TimeSpent).Execute();

        if (!passTime.IsSuccess)
        {
            SetError("PassTime: " + passTime.ErrorMessage);
        }
    }

    protected override Task FinalizeResult()
    {
        SetMessage($"{Actor.Name} have rested for {TimeSpent.TotalMinutes} minutes.");

        return Task.CompletedTask;
    }
}
