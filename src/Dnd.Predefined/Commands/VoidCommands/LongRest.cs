namespace Dnd.Predefined.Commands.VoidCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.GameActor;

public class LongRest : VoidCommand
{
    public LongRest(IGameActor character, TimeSpan? timeSpent) : base(character)
    {
        TimeSpent = timeSpent ?? TimeSpan.FromHours(8);
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
        SetMessage($"{Actor.Name} has rested for {TimeSpent.TotalHours} hours.");

        return Task.CompletedTask;
    }
}
