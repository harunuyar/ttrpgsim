namespace Dnd.System.CommandSystem.Commands.EventCommands;

using Dnd.System.Entities.Characters;

public class EndTurn : DndEventCommand
{
    public EndTurn(IEventListener eventListener, ICharacter character) : base(eventListener, character)
    {
    }

    public override void FinalizeEvent()
    {
        EventResult.SetMessage($"{Character.Name} has ended their turn");
    }
}
