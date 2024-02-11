namespace Dnd.CommandSystem.Commands.BooleanResultCommands;

using Dnd.Entities.Characters;

public class CanCastSpell : DndBooleanCommand
{
    public CanCastSpell(Character character) : base(character)
    {
    }

    public override void InitializeResult()
    {
        Result.SetValue("Default", false);
    }
}
