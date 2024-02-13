namespace Dnd.System.CommandSystem.Commands.BooleanResultCommands;

using Dnd.System.Entities.GameActors;

public class CanCastSpell : DndBooleanCommand
{
    public CanCastSpell(IGameActor character) : base(character)
    {
    }

    protected override void InitializeResult()
    {
        Result.SetValue("Default", false);
    }

    protected override void FinalizeResult()
    {
    }
}
