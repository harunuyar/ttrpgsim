namespace Dnd.System.CommandSystem.Commands.EventCommands;

using Dnd.System.Entities.Characters;

public class TakeTurn : DndEventCommand
{
    public TakeTurn(IEventListener eventListener, ICharacter character) : base(eventListener, character)
    {
    }

    protected override void InitializeEvent()
    {
    }

    protected override void FinalizeEvent()
    {
        EventResult.SetMessage($"It is {Character.Name}'s turn");
    }
}
