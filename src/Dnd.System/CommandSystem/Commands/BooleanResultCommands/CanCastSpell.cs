namespace Dnd.System.CommandSystem.Commands.BooleanResultCommands;

using Dnd.System.Entities.Characters;

public class CanCastSpell : DndBooleanCommand
{
    public CanCastSpell(ICharacter character) : base(character)
    {
    }

    public override void InitializeResult()
    {
        Result.SetValue("Default", false);
    }
}
