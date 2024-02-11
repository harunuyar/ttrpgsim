namespace Dnd.System.CommandSystem.Commands;

using Dnd.System.Entities.Characters;

public abstract class DndEventCommand : DndCommand
{
    protected DndEventCommand(ICharacter character) : base(character)
    {
    }
}
