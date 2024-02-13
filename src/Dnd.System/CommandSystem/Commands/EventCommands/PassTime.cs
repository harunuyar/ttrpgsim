namespace Dnd.System.CommandSystem.Commands.EventCommands;

using Dnd.System.Entities.Characters;

public class PassTime : DndEventCommand
{
    public PassTime(IEventListener eventListener, ICharacter character, TimeSpan timeSpan) : base(eventListener, character)
    {
        TimeSpan = timeSpan;
    }

    public TimeSpan TimeSpan { get;}

    protected override void FinalizeEvent()
    {
        EventResult.SetMessage($"{TimeSpan} has passed");
    }

    protected override void InitializeEvent()
    {
    }
}
